using HospitalAPI.Adapters;
using HospitalLibrary.Core.DomainService;
using HospitalLibrary.Core.DomainService.Interface;
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
        private readonly IPatientHealthInformationScoreService _patientHealthInformationScoreService;
        private readonly IPatientService _patientService;
        private readonly IPatientScoreService _patientScoreService;

        public PatientHealthController(IPatientHealthInformationService patientHealthInformationService, IPatientHealthInformationScoreService patientHealthInformationScoreService, IPatientService patientService, IPatientScoreService patientScoreService)
        {
            _patientHealthInformationService = patientHealthInformationService;
            _patientHealthInformationScoreService = patientHealthInformationScoreService;
            _patientService = patientService;
            _patientScoreService = patientScoreService;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("{personId}")]
        public ActionResult GetPatientHealthInformationByPersonId(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            PatientHealthInformation patientHealthInformation = _patientHealthInformationService.GetLatestByPatientId(patient.Id);
            if(patientHealthInformation == null)
            {
                return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient));
            }

            return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient, patientHealthInformation));
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("messages/{personId}")]
        public ActionResult GetPatientHealthInformationMessagesByPersonId(int personId)
        {
            PatientHealthInformation patientHealthInformation = _patientHealthInformationService.GetLatestByPatientId(_patientService.getPatientByPersonId(personId).Id);
            return Ok(patientHealthInformation.IsWithinNormalLimits());
        }
        

        [Authorize(Roles = "Patient")]
        [HttpPost("{personId}")]
        public ActionResult UpdatePatientHealthInformationByPersonId(PatientInfoDTO patientInfoDTO, int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            if (patient == null)
            {
                return BadRequest("Patient not found.");
            }

            PatientHealthInformation patientHealthInformation = PatientHealthAdapter.FromPatientInfoDTO(patientInfoDTO, patient);
            patientHealthInformation.Score = _patientHealthInformationScoreService.CalculatePatientHealthInforamtionScore(patientHealthInformation);
            _patientHealthInformationService.Create(patientHealthInformation);

            _patientService.Update(_patientScoreService.UpdatePatientHealtScore(patient, patientHealthInformation.Score));

            return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient, patientHealthInformation));
        }
    }
}
