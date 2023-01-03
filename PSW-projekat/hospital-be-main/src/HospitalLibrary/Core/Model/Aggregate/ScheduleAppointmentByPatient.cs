﻿using HospitalLibrary.Core.Model.Aggregate.Events;
using HospitalLibrary.Core.Model.Enums;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class ScheduleAppointmentByPatient : EventSourcedAggregate
    {
        public SchedulingStage Stage { get; set; } = SchedulingStage.beginning;
        public virtual Patient Patient { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public ScheduleAppointmentByPatient() { }

        //funkcija agregata
        public void ChooseAppointmentTime(String appointmentTime)
        {
            Causes(new PatientSelectedAppointmentTime(appointmentTime));
        }

        public void ChooseDoctor(string doctorName)
        {
            Causes(new PatientSelectedDoctor(doctorName));
        }

        public void ChooseSpecialization(string specialization)
        {
            Causes(new PatientSelectedDoctorSpecialization(specialization));
        }

        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event); //dodaje u listu promena
            Apply(@event);
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
        }

        private void When(PatientSelectedAppointmentTime patientSelectedAppointmentTime)
        {
            patientSelectedAppointmentTime.Aggregate = this;
            patientSelectedAppointmentTime.selectionTime = DateTime.Now;
        }

        private void When(PatientSelectedDoctorSpecialization patientSelectedDoctorSpecialization)
        {
            patientSelectedDoctorSpecialization.Aggregate = this;
            patientSelectedDoctorSpecialization.selectionTime = DateTime.Now;
        }

        private void When(PatientSelectedDoctor patientSelectedDoctor)
        {
            patientSelectedDoctor.Aggregate = this;
            patientSelectedDoctor.selectionTime = DateTime.Now;
        }
    }
}
