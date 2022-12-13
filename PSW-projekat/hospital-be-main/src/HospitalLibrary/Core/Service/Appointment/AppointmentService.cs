﻿using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace HospitalLibrary.Core.Service
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public IWorkingDayRepository workingDayRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository, IWorkingDayRepository workingDayRepository)
        {
            _appointmentRepository = appointmentRepository;
            this.workingDayRepository = workingDayRepository;
        }

        public bool InWorkingTime(Appointment entity, IEnumerable<WorkingDay> workingDays)
        {

            foreach (WorkingDay workingDay in workingDays)
            {
                DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), workingDay.Day.ToString());

                DateTime wStart = new DateTime(1, 1, 1, workingDay.StartTime.Hour, workingDay.StartTime.Minute, workingDay.StartTime.Second);
                DateTime wEnd = new DateTime(1, 1, 1, workingDay.EndTime.Hour, workingDay.EndTime.Minute, workingDay.EndTime.Second);
                DateTime aTime = new DateTime(1, 1, 1, entity.DateTime.Hour, entity.DateTime.Minute, entity.DateTime.Second);

                if ((dayOfWeek.Equals(entity.DateTime.DayOfWeek)) && wStart <= aTime && wEnd >= aTime)
                {
                    return true;
                }
            }
            return false;
        }


        public void Create(Appointment entity)
        {
            /*
             if (InWorkingTime(entity, workingDayRepository.GetAllWorkingDaysByUser(3)))
             {
                 entity.Deleted = false;
                 _appointmentRepository.Create(entity);
             }
             */
            entity.CancelationDate = DateTime.MinValue;
            entity.Deleted = false;
            _appointmentRepository.Create(entity);
        }

        public void Delete(Appointment entity)
        {
            try
            {
                SentEmail(entity);

                _appointmentRepository.Delete(entity);
            }
            catch (Exception e) { }
        }
        public void SentEmail(Appointment appointment)
        {
            string fromMail = "hospitalpswisa@gmail.com";
            string fromPassword = "uleoarfabsegnuxa";

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = "Termin za pregled";
            message.To.Add(appointment.Patient.Person.Email.Adress.ToString());
            message.Body = "<html><body> Vas termin: " + appointment.DateTime.ToString() + " za pregled je obrisan.</body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public void Update(Appointment entity)
        {
            
           // if (InWorkingTime(entity, workingDayRepository.GetAllWorkingDaysByUser(2)))
            {
                entity.Deleted = false;
                _appointmentRepository.Update(entity);
            }

        }

        public IEnumerable<AppointmentDto> GetAllByDoctor(int doctorId)
        {
            IEnumerable<Appointment> allAppointments = _appointmentRepository.GetAllByDoctor(doctorId);
            List<AppointmentDto> appointmentsDtos = new();

            foreach (Appointment appointment in allAppointments)
            {

                AppointmentDto appointmentDto = new AppointmentDto();


                PatientDto patientDto = new PatientDto();
                patientDto.Id = appointment.Patient.Id;
                patientDto.Name = appointment.Patient.Person.Name;
                patientDto.Surname = appointment.Patient.Person.Surname;

                DoctorDto doctorDto = new DoctorDto();
                doctorDto.Id = appointment.Doctor.Id;
                doctorDto.Name = appointment.Doctor.Person.Name;
                doctorDto.Surname = appointment.Doctor.Person.Surname;

                appointmentDto.Patient = patientDto;
                appointmentDto.Doctor = doctorDto;
                appointmentDto.DateTime = appointment.DateTime;

                appointmentDto.AppointmentId = appointment.Id;

                appointmentsDtos.Add(appointmentDto);
            }
            return appointmentsDtos;
            
        }

        public void Update(AppointmentDto appointmentDto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Appointment> GetAllAppointmentsForPatient(int patientId)
        {
            return _appointmentRepository.GetAllForPatient(patientId);
        }

        public IEnumerable<Patient> GetAllMaliciousPatients()
        {
            return _appointmentRepository.GetAllMaliciousPatients();
        }
    }
}
