using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
namespace HospitalAPI.Controllers.PublicApp
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class BedController : ControllerBase
    {
        private readonly IBedService _bedService;
        private readonly IPatientService _patientService;

        
        public BedController(IBedService bedService, IPatientService patientService)
        {
            _bedService = bedService;
            _patientService = patientService;
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var room = _bedService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, BedDto bedDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bedDto.Id)
            {
                return BadRequest();
            }

            bedDto.BedState = HospitalLibrary.Core.Model.Enums.BedState.taken;
            Patient patient = _patientService.GetById(bedDto.PatientDto.Id);

            bedDto.PatientDto = new PatientDto(patient.Id, patient.Person.Name, patient.Person.Surname,
                patient.Person.Email.Adress.ToString(), patient.Person.Role);

            
            Bed bed = _bedService.GetById(bedDto.Id);
            bed.Name = bedDto.Name;
            bed.BedState = HospitalLibrary.Core.Model.Enums.BedState.taken;
            bed.Quantity = bedDto.Quantity;
            bed.Patient = _patientService.GetById(bedDto.PatientDto.Id);
            

            try
            {
                _bedService.Update(bed);
            }
            catch
            {
                return BadRequest();

            }

            return Ok();
        }


    }
}
