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
        public DateTime AppointmentDoctorSelected { get; set; } //u kom momentu se dogadjaj dogodio

        public PatientSelectedDoctor(int aggregateId, String doctorName, DateTime appointmentDoctorSelected) : base(aggregateId)
        {
            DoctorName = doctorName;
            AppointmentDoctorSelected = appointmentDoctorSelected;
        }
    }
}
