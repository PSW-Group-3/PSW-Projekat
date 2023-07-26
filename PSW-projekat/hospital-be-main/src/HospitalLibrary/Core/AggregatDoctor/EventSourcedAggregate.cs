using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public abstract class EventSourcedAggregate : BaseModel
    {
        public virtual List<DomainEvent> Changes { get; set; }

        public EventSourcedAggregate()
        {
            Changes = new List<DomainEvent>();
        }

        public abstract void Apply(DomainEvent changes);
    }
}
