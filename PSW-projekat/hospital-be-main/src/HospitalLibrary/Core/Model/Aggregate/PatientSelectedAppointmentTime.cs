using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class PatientSelectedAppointmentTime : DomainEvent
    {
        public DateTime AppointmentTime { get; set; }
        public DateTime AppointmentTimeSelectedEvent { get; set; } //u kom momentu se dogadjaj dogodio

        public PatientSelectedAppointmentTime() {}

        public PatientSelectedAppointmentTime(int aggregateId, DateTime appointmentTime, DateTime appointmentTimeSelectedEvent) : base(aggregateId)
        {
            AppointmentTime = appointmentTime;
            AppointmentTimeSelectedEvent = appointmentTimeSelectedEvent;
        }
    }
}
