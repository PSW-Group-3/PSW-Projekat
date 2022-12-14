﻿using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
namespace HospitalAPI.Controllers.PublicApp
{
    //[Authorize]
    [EnableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationController : ControllerBase
    {

        private readonly IExaminationService _examinationService;
        private readonly IAppointmentService _appointmentService;
        private readonly ISymptomService _symptomService;
        private readonly IMedicineService _medicineService;


        public ExaminationController(IExaminationService examinationService, IAppointmentService appointmentService, ISymptomService symptomService,
            IMedicineService medicineService)
        {
            _examinationService = examinationService;
            _appointmentService = appointmentService;
            _symptomService = symptomService;
            _medicineService = medicineService;

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            List<ExaminationDto> examinationDto = new List<ExaminationDto>();
            foreach (var examination in _examinationService.GetAll())
            {
                PatientDto patientDto = new PatientDto(examination.Appointment.Patient.Id, examination.Appointment.Patient.Person.Name,
                  examination.Appointment.Patient.Person.Surname, examination.Appointment.Patient.Person.Email, 
                  examination.Appointment.Patient.Person.Role);

                DoctorDto doctorDto = new DoctorDto(examination.Appointment.Doctor.Id, examination.Appointment.Doctor.Person.Name,
                   examination.Appointment.Doctor.Person.Surname, examination.Appointment.Doctor.Person.Email, examination.Appointment.Doctor.Person.Role);

                AppointmentDto appointmentDto = new AppointmentDto(examination.Appointment.Id, examination.Appointment.DateTime, 
                    examination.Appointment.CancelationDate, patientDto, doctorDto);

                examinationDto.Add(new ExaminationDto(examination.Id, appointmentDto, examination.Prescriptions,
                    examination.Symptoms, examination.Report));

            }
            return Ok(examinationDto);
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var examination = _examinationService.GetById(id);

            PatientDto patientDto = new PatientDto(examination.Appointment.Patient.Id, examination.Appointment.Patient.Person.Name,
                  examination.Appointment.Patient.Person.Surname, examination.Appointment.Patient.Person.Email,
                  examination.Appointment.Patient.Person.Role);

            DoctorDto doctorDto = new DoctorDto(examination.Appointment.Doctor.Id, examination.Appointment.Doctor.Person.Name,
               examination.Appointment.Doctor.Person.Surname, examination.Appointment.Doctor.Person.Email, examination.Appointment.Doctor.Person.Role);

            AppointmentDto appointmentDto = new AppointmentDto(examination.Appointment.Id, examination.Appointment.DateTime,
                examination.Appointment.CancelationDate, patientDto, doctorDto);

            ExaminationDto examinationDto = new ExaminationDto(examination.Id, appointmentDto, examination.Prescriptions,
                    examination.Symptoms, examination.Report);

            if (examination == null)
            {
                return NotFound();
            }

            return Ok(examinationDto);
        }

        [HttpPost]
        public ActionResult Create(Examination examination)
        {
          
            examination.Appointment = _appointmentService.GetById(examination.Appointment.Id);
            List<Patient> patients = new List<Patient>();
            examination.Appointment.Doctor.Patients = patients;

            examination.Symptoms = _examinationService.GetHelpSymptoms(examination);

            foreach (Prescription prescription in examination.Prescriptions)
            {
                prescription.Medicines = _examinationService.GetHelpMedicines(prescription);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _examinationService.Create(examination);
            return Ok();
        }

    }
}
