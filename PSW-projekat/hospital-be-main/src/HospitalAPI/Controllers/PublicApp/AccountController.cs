using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.IntegrationConnection;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Model.MailRequests;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly UserManager<SecUser> _userManager;
        private readonly SignInManager<SecUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IIntegrationConnection _integrationConnection;
        private readonly AuthenticationDbContext _context;

        public AccountController(
                UserManager<SecUser> userManager,
                SignInManager<SecUser> signInManager,
                RoleManager<IdentityRole> roleManager,
                IEmailService emailService,
                IConfiguration configuration,
                IPersonService personService,
                IDoctorService doctorService,
                IPatientService patientService,
                IIntegrationConnection integrationConnection,
                AuthenticationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _personService = personService;
            _doctorService = doctorService;
            _patientService = patientService;
            _emailService = emailService;
            _configuration = configuration;
            _integrationConnection = integrationConnection;
            _context = context;
        }

        [HttpPost("Login")]
        public async Task<ActionResult> LoginAsync(LoginUserDto loginUserDto)
        {
            SecUser secUser = new SecUser();
            secUser = await _userManager.FindByNameAsync(loginUserDto.Username);
            if (secUser == null)
            {
                return BadRequest("Username or password is incorrect.");
            }
            if (secUser.IsBlocked)
            {
                return BadRequest("This user is blocked");
            }
            var statement = await _userManager.IsEmailConfirmedAsync(secUser);
            if (statement == true)
            {
                var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, true, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(loginUserDto.Username);
                    if (user != null && await _userManager.CheckPasswordAsync(user, loginUserDto.Password))
                    {
                        //var id = user.Claims.GetUserId();
                        var claims = await _userManager.GetClaimsAsync(user);
                        var userRoles = await _userManager.GetRolesAsync(user);

                        if(!((userRoles[0]=="Manager" && loginUserDto.Flag == "PZL")||
                           (userRoles[0] == "Doctor" && loginUserDto.Flag == "PZL")||
                           (userRoles[0] == "Patient" && loginUserDto.Flag == "PZP")))
                        {
                            return BadRequest("Wrong application.");
                        }

                        var authClaims = new List<Claim>
                        {
                            new Claim("Id", claims[0].Value),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                        };

                        foreach (var userRole in userRoles)
                        {
                            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                            authClaims.Add(new Claim("Role", userRole));
                        }

                        var token = GetToken(authClaims);

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        });
                    }
                }
                return BadRequest("Username or password is incorrect.");
            }
            return BadRequest("Email not confirmed");
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddDays(30),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }

        [HttpGet("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }


        [HttpGet("GetAllergiesAndDoctors")]
        public ActionResult GetAllergiesAndDoctors()
        {
            return Ok(_doctorService.GetAllergiesAndDoctors());
        }

        [AllowAnonymous]
        [HttpPost("CreateManager")]
        public async Task<IActionResult> CreateManager(CreateManagerDto createManagerDto)
        {
            bool patientRoleExists = await _roleManager.RoleExistsAsync("Manager");
            if (!patientRoleExists)
            {
                IdentityRole identityRole = new IdentityRole("Manager");
                var roleResult = await _roleManager.CreateAsync(identityRole);
            }

            Person user = new Person()
            {
                Id = 0,
                Name = createManagerDto.Name,
                Surname = createManagerDto.Surname,
                Role = Role.manager,
                Email = Email.Create(createManagerDto.Email),
                Gender = createManagerDto.Gender,
                BirthDate = Convert.ToDateTime(createManagerDto.BirthDate),
                Address = new Address()
                {
                    Street = createManagerDto.Street,
                    City = createManagerDto.City,
                    Number = createManagerDto.Number,
                    PostCode = createManagerDto.PostCode,
                    Township = createManagerDto.Township,
                },
                Jmbg = new Jmbg(createManagerDto.Jmbg)
            };
            user = _personService.RegisterPerson(user);
            SecUser secUser = new SecUser()
            {
                Id = user.Id,
                Email = createManagerDto.Email,
                UserName = createManagerDto.Username,
            };
            var registerUser = await _userManager.CreateAsync(secUser, createManagerDto.Password);
            if (registerUser != null)
            {
                await _userManager.AddToRoleAsync(secUser, "Manager");
                await _userManager.AddClaimAsync(secUser, new Claim("UserId", user.Id.ToString()));
            }

            return Ok(user);
        }

        [HttpPost("RegisterPatient")]
        public async Task<IActionResult> RegisterPatient(RegisterPatientDto regUser)
        {
            if (await _userManager.FindByNameAsync(regUser.Username) != null)
            {
                return BadRequest("Username is taken.");
            }

            if (!(await _roleManager.RoleExistsAsync("Patient")))
            {
                IdentityRole identityRole = new IdentityRole("Patient");
                var roleResult = await _roleManager.CreateAsync(identityRole);
            }

            Person user = new Person(regUser.Name, regUser.Surname, Email.Create(regUser.Email),
                            new Address(){
                                Street = regUser.Street,
                                City = regUser.City,
                                Number = regUser.Number,
                                PostCode = regUser.PostCode,
                                Township = regUser.Township
                            }, 
                            regUser.Gender, Convert.ToDateTime(regUser.BirthDate), Role.patient, new Jmbg(regUser.Jmbg));

            user = _personService.RegisterPerson(user);
            Doctor doctor = _doctorService.GetById(regUser.DoctorName.Id);

            Patient patient = new Patient(regUser.BloodType, user, doctor);

            patient = _patientService.RegisterPatient(patient);

        
            _patientService.AddAllergyToPatient(patient, regUser.Allergies);


            SecUser secUser = new SecUser()
            {
                Id = user.Id,
                Email = regUser.Email,
                UserName = regUser.Username,
                IsBlocked = false,
            };
            var registerUser = await _userManager.CreateAsync(secUser, regUser.Password);
            if (registerUser != null)
            {
                await _userManager.AddToRoleAsync(secUser, "Patient");
                await _userManager.AddClaimAsync(secUser, new Claim("UserId", user.Id.ToString()));
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(secUser);
            _emailService.SendEmailAsync(new AccountValidationMailRequest(user, regUser.Username, code));
            //await _userManager.ConfirmEmailAsync(secUser, code);

            return Ok();
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("CreateDoctor")]
        public async Task<IActionResult> CreateDoctor(CreateDoctorDto createDoctorDto)
        {
            bool patientRoleExists = await _roleManager.RoleExistsAsync("Doctor");
            if (!patientRoleExists)
            {
                IdentityRole identityRole = new IdentityRole("Doctor");
                var roleResult = await _roleManager.CreateAsync(identityRole);
            }

            Person user = new Person()
            {
                Id = 0,
                Name = createDoctorDto.Name,
                Surname = createDoctorDto.Surname,
                Role = Role.doctor,
                Email = Email.Create(createDoctorDto.Email),
                Gender = createDoctorDto.Gender,
                BirthDate = Convert.ToDateTime(createDoctorDto.BirthDate),
                Address = new Address()
                {
                    Street = createDoctorDto.Street,
                    City = createDoctorDto.City,
                    Number = createDoctorDto.Number,
                    PostCode = createDoctorDto.PostCode,
                    Township = createDoctorDto.Township,
                },
                Jmbg = new Jmbg(createDoctorDto.Jmbg)
            };
            user = _personService.RegisterPerson(user);

            Doctor doctor = new Doctor()
            {
                Specialization = createDoctorDto.Specialization,
                Person = user,
            };

            doctor = _doctorService.RegisterDoctor(doctor);

            SecUser secUser = new SecUser()
            {
                Id = user.Id,
                Email = createDoctorDto.Email,
                UserName = createDoctorDto.Username,
            };
            var registerUser = await _userManager.CreateAsync(secUser, createDoctorDto.Password);
            if (registerUser != null)
            {
                await _userManager.AddToRoleAsync(secUser, "Doctor");
                await _userManager.AddClaimAsync(secUser, new Claim("UserId", user.Id.ToString()));
            }

            return Ok(user);
        }

        [HttpGet("AccountConfirmation")]
        public async Task<IActionResult> AccountConfirmation(string username, string code)
        {
            SecUser secUser = new SecUser();
            secUser = await _userManager.FindByNameAsync(username);
            if (secUser == null)
            {
                return BadRequest("Bad credentials");
            }

            var statement = await _userManager.IsEmailConfirmedAsync(secUser);
            if (statement == true)
            {
                return BadRequest("Email already confirmed");
            }

            var a = await _userManager.ConfirmEmailAsync(secUser, code);

            return Ok();
        }


        [HttpPost("LoginBank")]
        public async Task<ActionResult> LoginBankAsync(LoginUserDto loginUserDto)
        {
            try
            {
                if (_integrationConnection.CheckIfExists(loginUserDto))
                {
                    var result = await _signInManager.PasswordSignInAsync(loginUserDto.Username, loginUserDto.Password, true, false);
                    if (result.Succeeded)
                    {
                        var user = await _userManager.FindByNameAsync(loginUserDto.Username);
                        if (user != null && await _userManager.CheckPasswordAsync(user, loginUserDto.Password))
                        {
                            //var id = user.Claims.GetUserId();
                            var claims = await _userManager.GetClaimsAsync(user);
                            var userRoles = await _userManager.GetRolesAsync(user);

                            if (!((userRoles[0] == "Manager" && loginUserDto.Flag == "PZL") ||
                                (userRoles[0] == "Doctor" && loginUserDto.Flag == "PZL") ||
                                (userRoles[0] == "Patient" && loginUserDto.Flag == "PZP") ||
                                (userRoles[0] == "BloodBank" && loginUserDto.Flag == "PZP")))
                            {
                                return BadRequest("Wrong application.");
                            }

                            var authClaims = new List<Claim>
                                {
                                    new Claim("Id", claims[0].Value),
                                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                                };

                            foreach (var userRole in userRoles)
                            {
                                authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                                authClaims.Add(new Claim("Role", userRole));
                                authClaims.Add(new Claim("Email",loginUserDto.Username));
                            }

                            var token = GetToken(authClaims);

                            return Ok(new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            });
                        }
                    }
                    else
                        return BadRequest("Username or password is incorrect.1");
                    return BadRequest("Username or password is incorrect.2");
                
            }
                else
                    return NotFound("Bank with these creditentials doesn't exist.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }




        }   

        [Authorize(Roles = "Manager")]
        [HttpPut("BlockUser/{personId}")]
        public async Task<ActionResult> BlockUserAsync(int personId)
        {
            var email = _personService.GetById(personId).Email;
            var secUser = await _userManager.FindByEmailAsync(email.Adress);
            if (secUser == null || secUser.IsBlocked)
                return BadRequest("User doesn't exist or is already blocked");
            secUser.BlockUser();
            _context.SaveChanges();
            return Ok();
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("UnblockUser/{personId}")]
        public async Task<ActionResult> UnblockUserAsync(int personId)
        {
            var email = _personService.GetById(personId).Email;
            var secUser = await _userManager.FindByEmailAsync(email.Adress);
            if (secUser == null || !secUser.IsBlocked)
                return BadRequest("User doesn't exist or is already unblocked");
            secUser.UnblockUser();
            _context.SaveChanges();
            return Ok();
        }
    }
}
