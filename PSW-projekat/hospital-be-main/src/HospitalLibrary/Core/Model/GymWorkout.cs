using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalLibrary.Core.Model
{
    public class GymWorkout : BaseModel
    {
        public double Score { get; set; }
        public WorkoutType WorkoutType { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Duration { get; set; }
        public string Description { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual List<Exercise> Exercises { get; set; }

        public GymWorkout() { }

        public GymWorkout(double score, WorkoutType type, DateTime date, TimeSpan duration, string description, Patient patient, List<Exercise> exercises)
        {
            if (!IsValid(type, date, duration)) throw new Exception("GymWorkout invalid!");

            WorkoutType = type;
            Date = date;
            Duration = duration;
            Description = description;
            Patient = patient;
            Exercises = exercises;
            Score = score;
        }

        private bool IsValid(WorkoutType type, DateTime date, TimeSpan duration)
        {
            if (type != WorkoutType.strength)
            {
                return false;
            }

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

        public SetsAndReps GetNumberOfSetsAndReps()
        {
            SetsAndReps setsAndReps = new(0, 0);
            foreach (Exercise exercise in Exercises)
            {
                setsAndReps.Sets += exercise.Sets;
                setsAndReps.Reps += exercise.Reps * exercise.Sets;
            }
            return setsAndReps;
        }
    }

    public struct SetsAndReps
    {
        public int Sets { get; set; }
        public int Reps { get; set; }

        public SetsAndReps(int sets, int reps)
        {
            Sets = sets;
            Reps = reps;
        }
    }
}
