using System;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class BackToDoctorSelection : DomainEvent
    {
        public BackToDoctorSelection()
        {
            phase = SchedulingStage.backToDoctor;
            selectionTime = DateTime.Now;
        }
    }
}
