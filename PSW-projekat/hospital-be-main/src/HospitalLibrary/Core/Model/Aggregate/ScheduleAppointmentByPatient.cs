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

        private readonly SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public ScheduleAppointmentByPatient() { }

        public ScheduleAppointmentByPatient(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository) 
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        //funkcija agregata
        public void ChooseAppointmentTime(DateTime appointmentTime)
        {
            //ubaci se izabrano vreme i onda na osnovu toga se napravi event za to
            //mozda provera da li tad moze da se rezervise termin
            Causes(new PatientSelectedAppointmentTime(Id, appointmentTime, DateTime.Now));
        }

        public void ChooseDoctor()
        {
            //ubaci se izabrano vreme i onda na osnovu toga se napravi event za to
            //mozda provera da li tad moze da se rezervise termin
            //Causes() pa neki domainEvent
        }

        public void ChooseSpecialization()
        {
            //ubaci se izabrano vreme i onda na osnovu toga se napravi event za to
            //mozda provera da li tad moze da se rezervise termin
            //Causes() pa neki domainEvent
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
            this.Changes.Add(patientSelectedAppointmentTime);
            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(this);
        }

        private void When(PatientSelectedDoctorSpecialization patientSelectedDoctorSpecialization)
        {
            //Ovde kazem tipa dodato je vreme i dodam ga u Appointment
        }

        private void When(PatientSelectedDoctor patientSelectedDoctor)
        {
            //Ovde kazem tipa dodato je vreme i dodam ga u Appointment
        }
    }
}
