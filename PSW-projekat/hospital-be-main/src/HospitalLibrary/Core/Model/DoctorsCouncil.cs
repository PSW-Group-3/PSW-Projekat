using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Core.Model
{
    public class DoctorsCouncil : BaseModel
    {
        public String Topic { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Duration { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        [NotMapped]
        public virtual ICollection<Specialization> Specializations { get; set; }

        public DoctorsCouncil() { }
        public DoctorsCouncil(String topic, DateTime start,DateTime end, int duration, ICollection<Doctor> doctors, ICollection<Specialization> specializations) 
        {
            if (Validate(topic, start, end, duration))
            {
                Topic = topic;
                Start = start;
                End = end;
                Duration = duration;
                Doctors = doctors;
                Specializations = specializations;
            }
            else
                throw new Exception("Invalid values");
            }

        public DoctorsCouncil(String topic, DateTime start, DateTime end, int duration, ICollection<Doctor> doctors)
        {
            if (Validate(topic, start, end, duration))
            {
                Topic = topic;
                Start = start;
                End = end;
                Duration = duration;
                Doctors = doctors;
            }
            else
                throw new Exception("Invalid values ");
    }

        private bool Validate(String topic, DateTime start, DateTime end, int duration)
        {
            if (duration < 0) {
                return false;
            }

            if (topic == "") {
                return false;
            }

            if (DateTime.Compare(start, DateTime.Now) < 0) {
                return false;
            }

            if (DateTime.Compare(end, start) < 0)
            {
                return false;
            }

            return true;
        }

    }
}
