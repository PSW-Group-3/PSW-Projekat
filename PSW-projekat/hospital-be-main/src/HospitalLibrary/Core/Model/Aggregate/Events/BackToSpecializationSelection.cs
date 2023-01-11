using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
