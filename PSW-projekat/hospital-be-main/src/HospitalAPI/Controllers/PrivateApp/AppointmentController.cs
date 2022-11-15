using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Controllers.PrivateApp
{
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]

    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;


        public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService, IPatientService patientService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _patientService = patientService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<AppointmentDto> appointmentDto = new List<AppointmentDto>();
            foreach (var appointment in _appointmentService.GetAll())
            {
                PatientDto patientDto = new PatientDto(appointment.Patient.Id, appointment.Patient.Person.Name,
                    appointment.Patient.Person.Surname, appointment.Patient.Person.Email, appointment.Patient.Person.Role);

                DoctorDto doctorDto = new DoctorDto(appointment.Doctor.Id, appointment.Doctor.Person.Name,
                   appointment.Doctor.Person.Surname, appointment.Doctor.Person.Email, appointment.Doctor.Person.Role);

                appointmentDto.Add(new AppointmentDto(appointment.Id, appointment.DateTime, patientDto, doctorDto));

            }

            return Ok(appointmentDto);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var appointment = _appointmentService.GetById(id);

            PatientDto patientDto = new PatientDto(appointment.Patient.Id, appointment.Patient.Person.Name,
                    appointment.Patient.Person.Surname, appointment.Patient.Person.Email, appointment.Patient.Person.Role);

            DoctorDto doctorDto = new DoctorDto(appointment.Doctor.Id, appointment.Doctor.Person.Name,
               appointment.Doctor.Person.Surname, appointment.Doctor.Person.Email, appointment.Doctor.Person.Role);

            AppointmentDto appointmentDto = new AppointmentDto(appointment.Id, appointment.DateTime, patientDto, doctorDto);

            if (appointment == null)
            {
                return NotFound();
            }

            return Ok(appointmentDto);
        }



        [HttpPost]
        public ActionResult Create(Appointment appointment)
        {
            //ovde dobijemo doktorov id
            appointment.Doctor = _doctorService.GetById(appointment.Doctor.Id);
            appointment.Patient = _patientService.GetById(appointment.Patient.Id);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _appointmentService.Create(appointment);
            return CreatedAtAction("GetById", new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, AppointmentDto appointmentDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != appointmentDto.AppointmentId)
            {
                return BadRequest();
            }

            Appointment appointment = _appointmentService.GetById(appointmentDto.AppointmentId);
            appointment.DateTime = appointmentDto.DateTime;

            try
            {
                _appointmentService.Update(appointment);
            }
            catch(Exception ex)
            {
                //return BadRequest();
                return BadRequest(ex.Message);

            }

            return Ok(appointment);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var appointment = _appointmentService.GetById(id);
            if (appointment == null)
            {
                return NotFound();
            }

            _appointmentService.Delete(appointment);
            return NoContent();
        }

        [HttpGet("doctor/{doctorId}")]
        public ActionResult GetAllByDoctor(int doctorId)
        {
            return Ok(_appointmentService.GetAllByDoctor(doctorId));
        }  
    }
}
