using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class DoctorsCouncilDto
    {
        public int Id { get; set; }
        public String Topic { get; set; }
        public DateTime Start { get; set; }
        public float Duration { get; set; }
        public virtual ICollection<DoctorDto> Doctors { get; set; }


        public DoctorsCouncilDto() { }

        public DoctorsCouncilDto(int id, string topic, DateTime start, float duration, ICollection<DoctorDto> doctors)
        {
            Id = id;
            Topic = topic;
            Start = start;
            Duration = duration;
            Doctors = doctors;
        }
    }

}
