using System;

namespace HospitalLibrary.Core.DTOs
{
    public class DateAndDoctorForNewAppointmentDto
    {
        public int DoctorId { get; set; }
        public DateTime ScheduledDate { get; set; }

        public DateAndDoctorForNewAppointmentDto() { }

        public DateAndDoctorForNewAppointmentDto(int doctorId, DateTime dateTime )
        {
            DoctorId = doctorId;
            ScheduledDate = dateTime;
        }
    }
}
