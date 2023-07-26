using System;

namespace HospitalLibrary.Core.DTOs
{
    public class ScheduleAppointmentDto
    {
        public DateTime ScheduledDate { get; set; }
        public int PersonId { get; set; }
        public SimpleDoctorDto DoctorDto { get; set; }

        public ScheduleAppointmentDto() { }
        public ScheduleAppointmentDto(DateTime dateTime, int patientId, SimpleDoctorDto doctor)
        {
            ScheduledDate = dateTime;
            PersonId = patientId;
            DoctorDto = doctor;
        }

    }
}
