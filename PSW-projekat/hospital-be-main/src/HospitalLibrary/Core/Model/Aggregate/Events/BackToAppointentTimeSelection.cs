using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
