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
        public DateTime AppointmentTimeSelected { get; set; } //u kom momentu se dogadjaj dogodio

        public PatientSelectedAppointmentTime(int aggregateId, DateTime appointmentTime, DateTime appointmentTimeSelected) : base(aggregateId)
        {
            AppointmentTime = appointmentTime;
            AppointmentTimeSelected = appointmentTimeSelected;
        }
    }
}
