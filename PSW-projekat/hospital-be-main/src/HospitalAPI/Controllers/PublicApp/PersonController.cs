using HospitalLibrary.Core.Service;
using HospitalLibrary.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.PublicApp
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly UserManager<SecUser> _userManager;
        private readonly PersonService _personService;

        public PersonController(PersonService personService, UserManager<SecUser> userManager)
        {
            _personService = personService;
            _userManager = userManager;
        }

        //[Authorize(Roles = "Manager")]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_personService.GetAll());
        }


        [HttpGet("patient/")]
        public ActionResult GetAllPatients()
        {
            return Ok(_personService.GetAllPatients());

        }

        [HttpGet("doctor/")]
        public ActionResult GetAllDoctors()
        {
            return Ok(_personService.GetAllDoctors());
        }

        [HttpGet("userInfo/")]
        public ActionResult GetUserInfo()
        {
            var id = User.Claims.GetUserId();

            return Ok(_personService.GetById(id));
        }
    }
}
