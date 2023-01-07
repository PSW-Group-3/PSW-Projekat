using HospitalLibrary.Core.Model.Aggregate.Events;
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

        public void ChooseAppointmentDate(String appointmentDate)
        {
            Causes(new PatientSelectedAppointmentDate(appointmentDate));
        }

        public void ChooseSpecialization(string specialization)
        {
            Causes(new PatientSelectedDoctorSpecialization(specialization));
        }

        public void BackToSpecializationChoosing()
        {
            Causes(new BackToSpecializationSelection());
        }

        public void BackToDoctorChoosing()
        {
            Causes(new BackToDoctorSelection());
        }

        public void BackToAppointmentTimeChoosing()
        {
            Causes(new BackToAppointentTimeSelection());
        }

        public void BackToAppointmentDateChoosing()
        {
            Causes(new BackToAppointmentDateSelection());
        }

        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
        }

        private void When(PatientSelectedAppointmentTime patientSelectedAppointmentTime)
        {
            patientSelectedAppointmentTime.Aggregate = this;
            this.Stage = SchedulingStage.timeChoosen;
        }

        private void When(PatientSelectedDoctorSpecialization patientSelectedDoctorSpecialization)
        {
            patientSelectedDoctorSpecialization.Aggregate = this;
            this.Stage = SchedulingStage.specChoosen;
        }

        private void When(PatientSelectedDoctor patientSelectedDoctor)
        {
            patientSelectedDoctor.Aggregate = this;
            this.Stage = SchedulingStage.doctorChoosen;
        }

        private void When(BackToSpecializationSelection backToSpecializationSelection)
        {
            backToSpecializationSelection.Aggregate = this;
            this.Stage = SchedulingStage.backToSpec;
        }

        private void When(BackToDoctorSelection backToDoctorSelection)
        {
            backToDoctorSelection.Aggregate = this;
            this.Stage = SchedulingStage.backToDoctor;
        }

        private void When(BackToAppointentTimeSelection backToAppointentTimeSelection)
        {
            backToAppointentTimeSelection.Aggregate = this;
            this.Stage = SchedulingStage.backToTime;
        }

        private void When(BackToAppointmentDateSelection backToAppointmentDateSelection)
        {
            backToAppointmentDateSelection.Aggregate = this;
            this.Stage = SchedulingStage.backToDate;
        }

        private void When(PatientSelectedAppointmentDate patientSelectedAppointmentDate)
        {
            patientSelectedAppointmentDate.Aggregate = this;
            this.Stage = SchedulingStage.dateChoosen;
        }
    }
}
