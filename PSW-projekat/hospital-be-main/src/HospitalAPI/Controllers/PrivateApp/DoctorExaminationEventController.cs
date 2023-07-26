﻿using HospitalAPI.DTO;
using HospitalLibrary.Core.AggregatDoctor;
using HospitalLibrary.Core.AggregatDoctor.use;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HospitalAPI.Controllers.PrivateApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorExaminationEventController : ControllerBase
    {
        private readonly DoctorExaminationEventsRepository _doctorExaminationEventsRepository;
        private readonly IDoctorService _doctorService;

        public DoctorExaminationEventController(DoctorExaminationEventsRepository doctorExaminationEventsRepository, IDoctorService doctorService)
        {
            _doctorExaminationEventsRepository = doctorExaminationEventsRepository;
            _doctorService = doctorService;
        }

        [HttpGet("DoctorExaminationAggregateStartTime/{doctorId}")]
        public ActionResult DoctorExaminationAggregateStartTime(int doctorId)
        {
            DoctorExamination doctorExamination = new DoctorExamination();
            doctorExamination = _doctorExaminationEventsRepository.Create(doctorExamination);
            doctorExamination.Doctor = _doctorService.GetById(doctorId);
            _doctorExaminationEventsRepository._context.SaveChanges();

            return Ok(doctorExamination.Id);
        }

        [HttpPost("DoctorExaminationAggregateEndTime")]
        public ActionResult DoctorExaminationAggregateEndTime(DoctorExaminationEventDTO doctorExaminationEventDTO)
        {
            DoctorExamination doctorExamination = _doctorExaminationEventsRepository.findById(doctorExaminationEventDTO.Id);
            doctorExamination.endTime = DateTime.Now;
            doctorExamination.Stage = ExaminationStage.eximinationFinished;
            _doctorExaminationEventsRepository._context.SaveChanges();

            return Ok();
        }

        [HttpPost("DoctorChoosingExaminationSymptoms")]
        public ActionResult DoctorChoosingExaminationSymptoms(DoctorExaminationEventDTO doctorExaminationEventDTO)
        {
            DoctorChoosingSymptoms doctorChoosingSymptoms = new DoctorChoosingSymptoms(_doctorExaminationEventsRepository) { };
            doctorChoosingSymptoms.Execute(doctorExaminationEventDTO.Id, doctorExaminationEventDTO.report);

            return Ok();
        }

        [HttpPost("DoctorChoosingExaminationReport")]
        public ActionResult DoctorChoosingExaminationReport(DoctorExaminationEventDTO doctorExaminationEventDTO)
        {
            DoctorChoosingReport doctorChoodsingReport = new DoctorChoosingReport(_doctorExaminationEventsRepository) { };
            doctorChoodsingReport.Execute(doctorExaminationEventDTO.Id, doctorExaminationEventDTO.report);

            return Ok();
        }

        [HttpPost("DoctorChoosingExaminationPrescriptions")]
        public ActionResult DoctorChoosingExaminationPrescriptions(DoctorExaminationEventDTO doctorExaminationEventDTO)
        {
            DoctorChoosingPrescriptions doctorChoosingPrescriptions = new DoctorChoosingPrescriptions(_doctorExaminationEventsRepository) { };
            doctorChoosingPrescriptions.Execute(doctorExaminationEventDTO.Id, doctorExaminationEventDTO.report);

            return Ok();
        }

       
        [HttpPost("BackToExaminationPerscriptiosChoosing")]
        public ActionResult BackToExaminationPerscriptiosChoosing(DoctorExaminationEventDTO doctorExaminationEventDTO)
        {
            BackToPrescriptionsChoosing backToSpecializationChoosing = new BackToPrescriptionsChoosing(_doctorExaminationEventsRepository) { };
            backToSpecializationChoosing.Execute(doctorExaminationEventDTO.Id);

            return Ok();
        }

        [HttpPost("BackToExaminationSymptomsChoosing")]
        public ActionResult BackToExaminationSymptomsChoosing(DoctorExaminationEventDTO doctorExaminationEventDTO)
        {
            BackToSymptomsChoosing backToDoctorChoosing = new BackToSymptomsChoosing(_doctorExaminationEventsRepository) { };
            backToDoctorChoosing.Execute(doctorExaminationEventDTO.Id);

            return Ok();
        }

        [HttpPost("BackToExaminationReportChoosing")]
        public ActionResult BackToExaminationReportChoosing(DoctorExaminationEventDTO doctorExaminationEventDTO)
        {
            BackToReportChoosing backToAppointmentTimeChoosing = new BackToReportChoosing(_doctorExaminationEventsRepository) { };
            backToAppointmentTimeChoosing.Execute(doctorExaminationEventDTO.Id);

            return Ok();
        }
    }
}
