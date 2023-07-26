using System;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class BackToReportWritten : DomainEvent
    {
        public BackToReportWritten()
        {
            phase = ExaminationStage.backToReort;
            selectionTime = DateTime.Now;
        }
    }
}
