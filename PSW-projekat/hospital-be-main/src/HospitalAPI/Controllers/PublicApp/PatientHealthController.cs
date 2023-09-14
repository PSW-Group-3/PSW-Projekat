using HospitalAPI.Adapters;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHealthController : ControllerBase
    {
        private readonly IPatientHealthInformationService _patientHealthInformationService;
        private readonly IPatientService _patientService;

        public PatientHealthController(IPatientHealthInformationService patientHealthInformationService, IPatientService patientService)
        {
            _patientHealthInformationService = patientHealthInformationService;
            _patientService = patientService;
        }

        [Authorize]
        [HttpGet("{personId}")]
        public ActionResult GetPatientHealthInformationByPersonId(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            PatientHealthInformation patientHealthInformation = _patientHealthInformationService.GetLatestByPatientId(patient.Id);

            if(patientHealthInformation == null)
            {
                return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient));
            }

            return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient, patientHealthInformation));
        }

        [Authorize]
        [HttpGet("messages/{personId}")]
        public ActionResult GetPatientHealthInformationMessagesByPersonId(int personId)
        {
            PatientHealthInformation patientHealthInformation = _patientHealthInformationService.GetLatestByPatientId(_patientService.getPatientByPersonId(personId).Id);
            return Ok(patientHealthInformation.IsWithinNormalLimits());
        }
        

        [Authorize]
        [HttpPost("{personId}")]
        public ActionResult UpdatePatientHealthInformationByPersonId(PatientInfoDTO patientInfoDTO, int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            PatientHealthInformation patientHealthInformation = PatientHealthAdapter.FromPatientInfoDTO(patientInfoDTO, patient);
            patient.UpdateHealthScore(patientHealthInformation.Score);

            _patientHealthInformationService.Create(patientHealthInformation);
            _patientService.Update(patient);

            return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient, patientHealthInformation));
        }
    }
}
