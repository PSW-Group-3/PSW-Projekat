using System;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class BackToAppointmentDateSelection : DomainEvent
    {
        public BackToAppointmentDateSelection()
        {
            phase = SchedulingStage.backToDate;
            selectionTime = DateTime.Now;
        }
    }
}
