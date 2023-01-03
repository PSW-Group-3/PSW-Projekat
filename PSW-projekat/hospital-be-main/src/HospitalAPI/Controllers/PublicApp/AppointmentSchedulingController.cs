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
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public AppointmentSchedulingController(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService, SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        [HttpPost("ChooseAppointmentDate")]
        public ActionResult ChooseAppointmentDate(DateTime date)
        {
            ChooseAppointmentTime chooseAppointmentTime = new ChooseAppointmentTime(_schedulingAppointmentEventsRepository) { };
            chooseAppointmentTime.Execute(3, date.ToString());
            
            return Ok();
        }

        [HttpPost("ChooseDoctorSpecialization")]
        public ActionResult ChooseDoctorSpecialization(String doctorSpecialization)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = new ScheduleAppointmentByPatient();
            scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.Create(scheduleAppointmentByPatient);

            ChooseSpecialization chooseSpecialization = new ChooseSpecialization(_schedulingAppointmentEventsRepository) { };
            chooseSpecialization.Execute(scheduleAppointmentByPatient.Id, doctorSpecialization);

            return Ok(scheduleAppointmentByPatient.Id);
        }

        [HttpPost("ChooseDoctor")]
        public ActionResult ChooseDoctor(String doctorName)
        {
            ChooseDoctor chooseDoctor = new ChooseDoctor(_schedulingAppointmentEventsRepository) { };
            chooseDoctor.Execute(3, doctorName);

            return Ok();
        }

        [HttpPost("BackToSpecializationChoosing")]
        public ActionResult BackToSpecializationChoosing()
        {
            BackToSpecializationChoosing backToSpecializationChoosing = new BackToSpecializationChoosing(_schedulingAppointmentEventsRepository) { };
            backToSpecializationChoosing.Execute(3);

            return Ok();
        }

        [HttpPost("BackToDoctorChoosing")]
        public ActionResult BackToDoctorChoosing()
        {
            BackToDoctorChoosing backToDoctorChoosing = new BackToDoctorChoosing(_schedulingAppointmentEventsRepository) { };
            backToDoctorChoosing.Execute(3);

            return Ok();
        }

        [HttpPost("BackToAppointmentTimeChoosing")]
        public ActionResult BackToAppointmentTimeChoosing()
        {
            BackToAppointmentTimeChoosing backToAppointmentTimeChoosing = new BackToAppointmentTimeChoosing(_schedulingAppointmentEventsRepository) { };    
            backToAppointmentTimeChoosing.Execute(3);

            return Ok();
        }
    }
}
