using System;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public abstract class DomainEvent : BaseModel
    {
        public virtual ScheduleAppointmentByPatient Aggregate { get; set; }
        public SchedulingStage phase { get; set; }
        public DateTime selectionTime { get; set; }
        public string selectedItem { get; set; }

        public DomainEvent() {}
    }
}
