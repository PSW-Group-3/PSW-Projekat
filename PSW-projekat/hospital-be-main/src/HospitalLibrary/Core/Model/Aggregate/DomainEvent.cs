using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public abstract class DomainEvent : BaseModel
    {
        public virtual ScheduleAppointmentByPatient Aggregate { get; set; }

        public DomainEvent()
        {

        }
        public DomainEvent(int aggregateId)
        {
            //Aggregate = new ScheduleAppointmentByPatient();
            //Aggregate.Id = aggregateId;
        }
    }
}
