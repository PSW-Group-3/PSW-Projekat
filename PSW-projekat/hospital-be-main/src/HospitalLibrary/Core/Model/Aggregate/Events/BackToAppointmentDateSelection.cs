using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class BackToAppointmentDateSelection : DomainEvent
    {
        public BackToAppointmentDateSelection()
        {
            phase = SchedulingStage.backToDate;
            selectionTime = DateTime.Now;
        }
    }
}
