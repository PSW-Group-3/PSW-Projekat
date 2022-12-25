using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public abstract class DomainEvent
    {
        public int Id { get; set; }

        public DomainEvent(int aggregateId)
        {
            Id = aggregateId;
        }
    }
}
