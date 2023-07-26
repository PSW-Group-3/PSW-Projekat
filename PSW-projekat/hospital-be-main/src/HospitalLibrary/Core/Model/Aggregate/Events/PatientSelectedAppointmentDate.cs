using System;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class PatientSelectedAppointmentDate : DomainEvent
    {
        public PatientSelectedAppointmentDate()
        {
            phase = SchedulingStage.dateChoosen;
            selectionTime = DateTime.Now;
        }

        public PatientSelectedAppointmentDate(string date)
        {
            phase = SchedulingStage.dateChoosen;
            selectedItem = date;
            selectionTime = DateTime.Now;
        }
    }
}
