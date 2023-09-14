using HospitalLibrary.Core.Model.Enums;
using System;

namespace HospitalLibrary.Core.Model
{
    public class Workout : BaseModel
    {
        public double Score { get; set; }
        public WorkoutType Type { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public virtual Patient Patient { get; set; }


        public Workout() { }

        public Workout(double score, WorkoutType type, DateTime date, TimeSpan duration, string description, Patient patient)
        {
            if (!IsValid(date, duration)) throw new Exception("Workout invalid!");

            Score = score;
            Type = type;
            Date = date;
            Duration = duration;
            Description = description;
            Patient = patient;
        }

        private bool IsValid(DateTime date, TimeSpan duration)
        {
            if (date.Date != DateTime.Today)
            {
                return false;
            }

            if (duration.TotalMinutes < 0)
            {
                return false;
            }

            return true;
        }  
    }
}
