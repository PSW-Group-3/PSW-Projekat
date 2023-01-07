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
        private readonly IPatientService _patientService;

        public AppointmentSchedulingController(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository, IPatientService patientService)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
            _patientService = patientService;
        }

        [HttpGet("AppointmentSchedulingAggregateStartTime")]
        public ActionResult AppointmentSchedulingAggregateStartTime(int patientId)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = new ScheduleAppointmentByPatient();
            scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.Create(scheduleAppointmentByPatient);
            scheduleAppointmentByPatient.Patient = _patientService.GetById(patientId);
            _schedulingAppointmentEventsRepository._context.SaveChanges();

            return Ok(scheduleAppointmentByPatient.Id);
        }

        [HttpPost("AppointmentSchedulingAggregateEndTime")]
        public ActionResult AppointmentSchedulingAggregateEndTime(int id)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);
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
        public ActionResult BackToSpecializationChoosing(int id)
        {
            BackToSpecializationChoosing backToSpecializationChoosing = new BackToSpecializationChoosing(_schedulingAppointmentEventsRepository) { };
            backToSpecializationChoosing.Execute(id);

            return Ok();
        }

        [HttpPost("BackToDoctorChoosing")]
        public ActionResult BackToDoctorChoosing(int id)
        {
            BackToDoctorChoosing backToDoctorChoosing = new BackToDoctorChoosing(_schedulingAppointmentEventsRepository) { };
            backToDoctorChoosing.Execute(id);

            return Ok();
        }

        [HttpPost("BackToAppointmentTimeChoosing")]
        public ActionResult BackToAppointmentTimeChoosing(int id)
        {
            BackToAppointmentTimeChoosing backToAppointmentTimeChoosing = new BackToAppointmentTimeChoosing(_schedulingAppointmentEventsRepository) { };    
            backToAppointmentTimeChoosing.Execute(id);

            return Ok();
        }

        [HttpPost("BackToAppointmentDateChoosing")]
        public ActionResult BackToAppointmentDateChoosing(int id)
        {
            BackToAppointmentDateChoosing backToAppointmentDATEChoosing = new BackToAppointmentDateChoosing(_schedulingAppointmentEventsRepository) { };
            backToAppointmentDATEChoosing.Execute(id);

            return Ok();
        }
    }
}
