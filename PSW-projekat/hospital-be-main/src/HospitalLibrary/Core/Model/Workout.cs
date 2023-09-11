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

        public Workout(WorkoutType type, DateTime date, TimeSpan duration, string description, Patient patient)
        {
            if (!IsValid(date, duration)) throw new Exception("Workout invalid!");

            Type = type;
            Date = date;
            Duration = duration;
            Description = description;
            Patient = patient;
            Score = CalculateScore();
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

        private double CalculateScore()
        {
            double score = GetWorkoutScore(Type);

            if (Duration.TotalMinutes > 60)
            {
                score *= Duration.TotalMinutes / 60;
            }

            return score;
        }

        private static int GetWorkoutScore(WorkoutType type)
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
                WorkoutType.strength => 3,
                _ => 0,
            };

            return score;
        }
    }
}
