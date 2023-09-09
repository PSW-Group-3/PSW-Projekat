using HospitalAPI.Adapters;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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

        //[Authorize]
        [HttpGet("healthinfo/{personId}")]
        public ActionResult GetPatientHealthInformationByPersonId(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);

            return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient, _patientHealthInformationService.GetLatestByPatientId(patient.Id)));
        }

        //[Authorize]
        [HttpGet("healthinfo/messages/{personId}")]
        public ActionResult GetPatientHealthInformationMessagesByPersonId(int personId)
        {
            PatientHealthInformation patientHealthInformation = _patientHealthInformationService.GetLatestByPatientId(_patientService.getPatientByPersonId(personId).Id);
            List<String> messages = patientHealthInformation.IsWithinNormalLimits();

            return Ok(new PatientHealthInforamationMessagesDTO { BloodPressureMessage = messages[2], HeightMessage = messages[1], WeightMessage = messages[0] });
        }
        

        //[Authorize]
        [HttpPut("healthinfo/{personId}")]
        public ActionResult UpdatePatientHealthInformationByPersonId(PatientInfoDTO patientInfoDTO, int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);
            //TODO: pitaj profesora, da li uzmimati u obzir prethodno stanje? Kako se ponasati u ekstremnim situacijama?
            PatientHealthInformation patientHealthInformation = PatientHealthAdapter.FromPatientInfoDTO(patientInfoDTO, patient);
            patient.UpdateHealthScore(patientHealthInformation.HealthScoreDelta);

            _patientHealthInformationService.Create(patientHealthInformation);
            _patientService.Update(patient);

            return Ok(PatientHealthAdapter.ToPatientInfoDTO(patient, patientHealthInformation));
        }
    }
}
