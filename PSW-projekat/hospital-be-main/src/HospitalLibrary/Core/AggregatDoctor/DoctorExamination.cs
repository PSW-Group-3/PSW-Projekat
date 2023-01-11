using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public class DoctorExamination : EventSourcedAggregate
    {
        public ExaminationStage Stage { get; set; } = ExaminationStage.beginning;
        public virtual Doctor Doctor { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public DoctorExamination() { }

        public override void Apply(DomainEvent changes)
        {
            throw new NotImplementedException();
        }
    }
}
