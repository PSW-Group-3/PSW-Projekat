using System;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class BackToPrescriptionsSelection : DomainEvent
    {
        public BackToPrescriptionsSelection()
        {
            phase = ExaminationStage.backToPrescription;
            selectionTime = DateTime.Now;
        }
    }
}
