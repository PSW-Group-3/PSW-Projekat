using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
