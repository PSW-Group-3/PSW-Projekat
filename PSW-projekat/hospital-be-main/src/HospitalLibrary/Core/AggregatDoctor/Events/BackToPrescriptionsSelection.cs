using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
