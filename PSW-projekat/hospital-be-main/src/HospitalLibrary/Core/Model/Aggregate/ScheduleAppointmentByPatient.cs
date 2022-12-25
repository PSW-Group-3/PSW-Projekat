using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class ScheduleAppointmentByPatient : EventSourcedAggregate
    {
        public SchedulingStage _stage; 

        
        public ScheduleAppointmentByPatient()
        {
            _stage = SchedulingStage.beginning;
        }

        //funkcija agregata
        public void ChooseAppointmentTime()
        {
            //ubaci se izabrano vreme i onda na osnovu toga se napravi event za to
            //mozda provera da li tad moze da se rezervise termin
            //Causes() pa neki domainEvent
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
            //Ovde kazem tipa dodato je vreme i dodam ga u Appointment
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
