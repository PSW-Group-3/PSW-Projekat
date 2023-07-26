using HospitalLibrary.Core.Model;
using System;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public abstract class DomainEvent : BaseModel
    {
        public virtual DoctorExamination Aggregate { get; set; }
        public ExaminationStage phase { get; set; }
        public DateTime selectionTime { get; set; }
        //public string selectedItem { get; set; }

        public DomainEvent() { }
    }
}
