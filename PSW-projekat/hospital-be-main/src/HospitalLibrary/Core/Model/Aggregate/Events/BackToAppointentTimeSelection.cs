using System;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class BackToAppointentTimeSelection : DomainEvent
    {
        public BackToAppointentTimeSelection()
        {
            phase = SchedulingStage.backToTime;
            selectionTime = DateTime.Now;
        }
    }
}
