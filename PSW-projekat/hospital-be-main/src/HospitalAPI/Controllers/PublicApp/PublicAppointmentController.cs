using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Controllers.PublicApp
{
    [Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]

    public class PublicAppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;

        public PublicAppointmentController(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [HttpPost("ScheduleAppointment")]
        public ActionResult ScheduleAppointment(ScheduleAppointmentDto appointmentDto)
        {
            Doctor doctor = _doctorService.GetById(appointmentDto.DoctorDto.Id);
            Patient patient = _patientService.getPatientByPersonId(appointmentDto.PersonId);

            if (doctor == null || patient == null)
            {
                return BadRequest();
            }

            Appointment appointment = new Appointment() { 
                Doctor = doctor,
                Patient = patient,
                DateTime = appointmentDto.ScheduledDate.AddHours(1),
                CancelationDate = null
            };

            //if(_appointmentService.

            _appointmentService.ScheduleAppointment(appointment);
            return Ok();
        }
        
        [HttpPut("GetFreeAppointmentsForDoctorByDate")]
        public ActionResult GetFreeAppointmentsForDoctor(DateAndDoctorForNewAppointmentDto dto)
        {
            List<string> appointmentTimes = _appointmentService.GetFreeAppointmentsForDoctor(dto.DoctorId, dto.ScheduledDate.AddHours(1));
            return Ok(appointmentTimes);
        }

        [HttpGet("GetPatientAppointments/{personId}")]
        public ActionResult GetPatientAppointments(int personId)
        {
            Patient patient = _patientService.getPatientByPersonId(personId);

            var patientAppointments = _appointmentService.GetAllAppointmentsForPatient(patient.Id);
            List<PatientAppointmentsDto> patientAppointmentsList = new List<PatientAppointmentsDto>();
            PatientAppointmentsDto patientAppointmentsDto = new PatientAppointmentsDto();

            foreach (var app in patientAppointments)
            {
                if (app.CancelationDate == null)
                {
                    if (app.DateTime.CompareTo(DateTime.Now) < 0)
                    {
                        patientAppointmentsDto = new PatientAppointmentsDto()
                        {
                            AppointmentId = app.Id,
                            DoctorFullName = app.Doctor.Person.Name + " " + app.Doctor.Person.Surname,
                            AppointmentTime = app.DateTime,
                            AppointmentStatus = "Finished"
                        };
                    }
                    else
                    {
                        patientAppointmentsDto = new PatientAppointmentsDto()
                        {
                            AppointmentId = app.Id,
                            DoctorFullName = app.Doctor.Person.Name + " " + app.Doctor.Person.Surname,
                            AppointmentTime = app.DateTime,
                            AppointmentStatus = "Upcoming"
                        };
                    }

                }
                else if (app.CancelationDate != null)
                {
                    patientAppointmentsDto = new PatientAppointmentsDto()
                    {
                        AppointmentId = app.Id,
                        DoctorFullName = app.Doctor.Person.Name + " " + app.Doctor.Person.Surname,
                        AppointmentTime = app.DateTime,
                        AppointmentStatus = "Cancelled"
                    };
                }
                patientAppointmentsList.Add(patientAppointmentsDto);
            }
            return Ok(patientAppointmentsList);
        }

        [HttpPut("CancelAppointment/{appointmentId}")]
        public ActionResult CancelAppointment(int appointmentId)
        {
            var appointment = _appointmentService.GetById(appointmentId);
            if (appointment == null || appointment.CancelationDate != null || appointment.DateTime < DateTime.Now.AddDays(1))
            {
                return BadRequest();
            }
            appointment.CancelationDate = DateTime.Now;
            _appointmentService.Update(appointment);
            return Ok();
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("GetAllDoctorsForCreatingAppointment")]
        public ActionResult GetAllDoctorsForCreatingAppointment()
        {
            return Ok(_doctorService.GetAllDoctorsForCreatingAppointment());
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("GetAllAvailableAppointmentsForCreatingAppointment")]
        public ActionResult GetAllAvailableAppointmentsForCreatingAppointment(
            CheckAvailableAppontmentDto checkAvailableAppontment
        )
        {
            List<AppointmentsAvailableForCreatingAppointment> appointments = _appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontment);
            if (appointments == null)
                return BadRequest();
            return Ok(appointments);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost("CreateCustomAppointment")]
        public ActionResult CreateCustomAppointment(
            CustomAppointmentForCreatingDto checkAppointment
        )
        {
            return Ok(_appointmentService.CreateCustomAppointment(checkAppointment));
        }
    }
}
