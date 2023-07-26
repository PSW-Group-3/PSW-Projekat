using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.PrivateApp
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]

    public class PrivatePatientController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly UserManager<SecUser> _userManager;


        public PrivatePatientController(IAppointmentService appointmentService, UserManager<SecUser> userManager)
        {
            _appointmentService = appointmentService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllMaliciousPatientsAsync()
        {
            var maliciousPatients = _appointmentService.GetAllMaliciousPatients();
            List<MaliciousPatientDto> maliciousPatientsDto = new List<MaliciousPatientDto>();
            foreach(Patient mp in maliciousPatients)
            {
                var secUser = await _userManager.FindByEmailAsync(mp.Person.Email.Adress);
                MaliciousPatientDto patientDto = new MaliciousPatientDto(mp.Person.Id, mp.Person.Name + " " + mp.Person.Surname, secUser.UserName, secUser.IsBlocked?"Blocked":"Not blocked");
                maliciousPatientsDto.Add(patientDto);
            }

            return Ok(maliciousPatientsDto);
        }

    }
}
