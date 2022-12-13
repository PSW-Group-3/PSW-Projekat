﻿using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IAppointmentService : IService<Appointment>
    {
        IEnumerable<AppointmentDto> GetAllByDoctor(int doctorId);
        void Update(AppointmentDto appointmentDto);
        void SentEmail(Appointment appointment);
        IEnumerable<Appointment> GetAllAppointmentsForPatient(int patientId);
        IEnumerable<Patient> GetAllMaliciousPatients();
        void ScheduleAppointment(Appointment appointment);
        List<string> GetFreeAppointmentsForDoctor(int doctorId, DateTime scheduledDate);
    }
}
