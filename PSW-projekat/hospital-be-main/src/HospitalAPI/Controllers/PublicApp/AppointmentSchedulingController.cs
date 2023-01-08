using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Aggregate;
using HospitalLibrary.Core.Model.Aggregate.useCases;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace HospitalAPI.Controllers.PublicApp
{
    //[Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentSchedulingController : Controller
    {
        private readonly SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;
        private readonly SchedulingStatisticsService _schedulingStatisticsService;
        private readonly IPatientService _patientService;

        public AppointmentSchedulingController(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository, SchedulingStatisticsService schedulingStatisticsService, IPatientService patientService)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
            _schedulingStatisticsService = schedulingStatisticsService;
            _patientService = patientService;
        }

        [HttpGet("AppointmentSchedulingAggregateStartTime/{patientId}")]
        public ActionResult AppointmentSchedulingAggregateStartTime(int patientId)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = new ScheduleAppointmentByPatient();
            scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.Create(scheduleAppointmentByPatient);
            scheduleAppointmentByPatient.Patient = _patientService.getPatientByPersonId(patientId);
            _schedulingAppointmentEventsRepository._context.SaveChanges();

            return Ok(scheduleAppointmentByPatient.Id);
        }

        [HttpPost("AppointmentSchedulingAggregateEndTime")]
        public ActionResult AppointmentSchedulingAggregateEndTime(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(appointmentSchedulingEventDTO.Id);
            scheduleAppointmentByPatient.endTime = DateTime.Now;
            scheduleAppointmentByPatient.Stage = SchedulingStage.appointmentScheduled;
            _schedulingAppointmentEventsRepository._context.SaveChanges();

            return Ok();
        }

        [HttpPost("ChooseAppointmentTime")]
        public ActionResult ChooseAppointmentTime(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            ChooseAppointmentTime chooseAppointmentTime = new ChooseAppointmentTime(_schedulingAppointmentEventsRepository) { };
            chooseAppointmentTime.Execute(appointmentSchedulingEventDTO.Id, appointmentSchedulingEventDTO.SelectedItem);
            
            return Ok();
        }

        [HttpPost("ChooseAppointmentDate")]
        public ActionResult ChooseAppointmentDate(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            ChooseAppointmentDate chooseAppointmentDate = new ChooseAppointmentDate(_schedulingAppointmentEventsRepository) { };
            chooseAppointmentDate.Execute(appointmentSchedulingEventDTO.Id, appointmentSchedulingEventDTO.SelectedItem);

            return Ok();
        }

        [HttpPost("ChooseDoctorSpecialization")]
        public ActionResult ChooseDoctorSpecialization(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            ChooseSpecialization chooseSpecialization = new ChooseSpecialization(_schedulingAppointmentEventsRepository) { };
            chooseSpecialization.Execute(appointmentSchedulingEventDTO.Id, appointmentSchedulingEventDTO.SelectedItem);

            return Ok();
        }

        [HttpPost("ChooseDoctor")]
        public ActionResult ChooseDoctor(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            ChooseDoctor chooseDoctor = new ChooseDoctor(_schedulingAppointmentEventsRepository) { };
            chooseDoctor.Execute(appointmentSchedulingEventDTO.Id, appointmentSchedulingEventDTO.SelectedItem);

            return Ok();
        }

        [HttpPost("BackToSpecializationChoosing")]
        public ActionResult BackToSpecializationChoosing(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            BackToSpecializationChoosing backToSpecializationChoosing = new BackToSpecializationChoosing(_schedulingAppointmentEventsRepository) { };
            backToSpecializationChoosing.Execute(appointmentSchedulingEventDTO.Id);

            return Ok();
        }

        [HttpPost("BackToDoctorChoosing")]
        public ActionResult BackToDoctorChoosing(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            BackToDoctorChoosing backToDoctorChoosing = new BackToDoctorChoosing(_schedulingAppointmentEventsRepository) { };
            backToDoctorChoosing.Execute(appointmentSchedulingEventDTO.Id);

            return Ok();
        }

        [HttpPost("BackToAppointmentTimeChoosing")]
        public ActionResult BackToAppointmentTimeChoosing(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            BackToAppointmentTimeChoosing backToAppointmentTimeChoosing = new BackToAppointmentTimeChoosing(_schedulingAppointmentEventsRepository) { };    
            backToAppointmentTimeChoosing.Execute(appointmentSchedulingEventDTO.Id);

            return Ok();
        }

        [HttpPost("BackToAppointmentDateChoosing")]
        public ActionResult BackToAppointmentDateChoosing(AppointmentSchedulingEventDTO appointmentSchedulingEventDTO)
        {
            BackToAppointmentDateChoosing backToAppointmentDATEChoosing = new BackToAppointmentDateChoosing(_schedulingAppointmentEventsRepository) { };
            backToAppointmentDATEChoosing.Execute(appointmentSchedulingEventDTO.Id);

            return Ok();
        }

        [HttpGet("GetAllEventStatistics")]
        public ActionResult GetAllEventStatistics()
        {
            SchedulingStatisticsDTO dto = _schedulingStatisticsService.GetAllEventStatistics();
            return Ok(dto);
        }

    }
}
