using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
