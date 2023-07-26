using System;

namespace HospitalLibrary.Core.DTOs
{
    public class PatientAppointmentsDto
    {
        public int AppointmentId { get; set; }
        public String DoctorFullName { get; set; }
        public DateTime AppointmentTime { get; set; }
        public String AppointmentStatus { get; set; }
    }
}
