using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class PatientSelectedDoctor : DomainEvent
    {
        public String DoctorName { get; set; }
        public DateTime AppointmentDoctorSelectedEvent { get; set; } //u kom momentu se dogadjaj dogodio

        public PatientSelectedDoctor() { }
        public PatientSelectedDoctor(int aggregateId, String doctorName, DateTime appointmentDoctorSelectedEvent) : base(aggregateId)
        {
            DoctorName = doctorName;
            AppointmentDoctorSelectedEvent = appointmentDoctorSelectedEvent;
        }
    }
}
