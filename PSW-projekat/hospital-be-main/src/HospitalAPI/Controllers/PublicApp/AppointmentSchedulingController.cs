﻿using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Aggregate;
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
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = new ScheduleAppointmentByPatient(_schedulingAppointmentEventsRepository);
            scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.Create(scheduleAppointmentByPatient);

            ChooseAppointmentTime chooseAppointmentTime = new ChooseAppointmentTime(_schedulingAppointmentEventsRepository) { };
            chooseAppointmentTime.Execute(scheduleAppointmentByPatient.Id, date);
            
            return Ok(scheduleAppointmentByPatient);
        }

        [HttpPost("ChooseDoctorSpecialization")]
        public ActionResult ChooseDoctorSpecialization(String doctorSpecialization)
        {
            return Ok();
        }

        [HttpPost("ChooseDoctor")]
        public ActionResult ChooseDoctor(String doctorNAme)
        {
            return Ok();
        }
    }
}
