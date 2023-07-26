using System;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class BackToSpecializationSelection : DomainEvent
    {
        public BackToSpecializationSelection()
        {
            phase = SchedulingStage.backToSpec;
            selectionTime = DateTime.Now;
        }
    }
}
