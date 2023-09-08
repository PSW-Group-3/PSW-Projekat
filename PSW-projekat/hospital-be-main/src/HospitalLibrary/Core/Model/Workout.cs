using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Workout(WorkoutType type, DateTime date, TimeSpan duration, string description, Patient patient)
        {
            if (!IsValid(date, duration)) throw new Exception("Workout invalid!");

            Score = CalculateScore(type);
            Type = type;
            Date = date;
            Duration = duration;
            Description = description;
            Patient = patient;
        }

        protected bool IsValid(DateTime date, TimeSpan duration)
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

        virtual protected double CalculateScore(WorkoutType type)
        {
            double score = GetWorkoutScore(type);

            if (Duration.Minutes > 60)
            {
                score *= Duration.TotalMinutes / 60;
            }

            return score;
        }

        public static int GetWorkoutScore(WorkoutType type)
        {
        int score = type switch
            {
                WorkoutType.walking => 1,
                WorkoutType.jogging => 2,
                WorkoutType.swimming => 2,
                WorkoutType.sports => 2,
                WorkoutType.cardio => 3,
                WorkoutType.running => 3,
                WorkoutType.cycling => 3,
                WorkoutType.strenght => 3,
                _ => 0,
            };

            return score;
        }
    }
}
