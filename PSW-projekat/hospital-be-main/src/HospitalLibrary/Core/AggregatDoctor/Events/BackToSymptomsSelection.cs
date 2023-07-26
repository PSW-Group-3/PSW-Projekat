using System;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class BackToSymptomsSelection : DomainEvent
    {
        public BackToSymptomsSelection()
        {
            phase = ExaminationStage.backToSymptoms;
            selectionTime = DateTime.Now;
        }
    }
}
