using HospitalAPI.Adapters;
using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service.CouncilOfDoctors;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Controllers.PrivateApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class CouncilOfDoctorsController : ControllerBase
    {
        private readonly ICouncilOfDoctorsService _councilOfDoctorsService;

        public CouncilOfDoctorsController(ICouncilOfDoctorsService councilOfDoctorsService)
        {
            this._councilOfDoctorsService = councilOfDoctorsService;
        }

        [HttpPost]
        public ActionResult Create(DoctorCouncilDTO testCase)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                DoctorsCouncil council = DoctorsCouncilAdapter.FromDTO(testCase);

                _councilOfDoctorsService.Create(council);
                return Ok(testCase);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult GetAll()
        {

            List<DoctorsCouncilDto> councilsDto = new List<DoctorsCouncilDto>();

            foreach (var council in _councilOfDoctorsService.GetAll())
            {
                ICollection<DoctorDto> doctorDtos = new List<DoctorDto>();

                foreach (var doctor in council.Doctors)
                {

                    doctorDtos.Add(new DoctorDto(doctor.Id, doctor.Person.Name, doctor.Person.Surname, doctor.Person.Email.Adress,
                                                 doctor.Person.Role));
                    
                }

                councilsDto.Add(new DoctorsCouncilDto(council.Id, council.Topic, council.Start, council.Duration, doctorDtos));
            }

            return Ok(councilsDto);
        }


    }
}

