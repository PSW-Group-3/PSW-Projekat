using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class PatientSelectedDoctorSpecialization : DomainEvent
    {
        public String DoctorSpecialization { get; set; }
        public DateTime AppointmentDoctorSpecializationSelected { get; set; } //u kom momentu se dogadjaj dogodio

        public PatientSelectedDoctorSpecialization(int aggregateId, String doctorSpecialization, DateTime appointmentDoctorSpecializationSelected) : base(aggregateId)
        {
            DoctorSpecialization = doctorSpecialization;
            AppointmentDoctorSpecializationSelected = appointmentDoctorSpecializationSelected; 
        }
    }
}
