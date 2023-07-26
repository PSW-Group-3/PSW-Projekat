using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalLibrary.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HospitalAPI.Controllers.PublicApp
{
    //[Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly UserManager<SecUser> _userManager;
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService, UserManager<SecUser> userManager)
        {
            _personService = personService;
            _userManager = userManager;
        }

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

        [Authorize(Roles ="Manager")]
        [HttpGet("userInfo/{id}")]
        public ActionResult GetUserInfo(String id)
        {
            //var id = User.Claims.GetUserId();
            Person person = _personService.GetById(int.Parse(id));

            return Ok(person);
        }


    }
}
