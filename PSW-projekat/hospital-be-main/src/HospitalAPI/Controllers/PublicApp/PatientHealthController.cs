using HospitalAPI.Adapters;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers.PublicApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientHealthController : ControllerBase
    {
        private readonly IPatientHealthInformationService _patientHealthInformationService;
        private readonly IPatientService _patientService;
        private readonly PatientHealthAdapter _patientHealthAdapter;

        public PatientHealthController(IPatientHealthInformationService patientHealthInformationService, IPatientService patientService, PatientHealthAdapter patientHealthAdapter)
        {
            _patientHealthInformationService = patientHealthInformationService;
            _patientService = patientService;
            _patientHealthAdapter = patientHealthAdapter;
        }

        //[Authorize]
        [HttpGet("healthscore/{personId}")]
        public ActionResult GetHealthScoreByPersonId(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);

            return Ok(_patientHealthAdapter.ToPatientInfoDTO(patient, _patientHealthInformationService.GetLatestByPatientId(patient.Id)));
        }



    }
}
