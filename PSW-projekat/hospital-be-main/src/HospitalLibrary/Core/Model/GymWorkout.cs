using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class GymWorkout : Workout
    {
        public virtual List<Exercise> Exercises { get; set; }

        public GymWorkout() { }

        public GymWorkout(WorkoutType type, DateTime date, TimeSpan duration, string description, Patient patient, List<Exercise> exercises)
        {
            if (!IsValid(date, duration)) throw new Exception("GymWorkout invalid!");

            Type = type;
            Date = date;
            Duration = duration;
            Description = description;
            Patient = patient;
            Exercises = exercises;
            Score = CalculateScore();
        }

        protected override double CalculateScore()
        {
            double score = GetWorkoutScore(Type);
            SetsAndReps setsAndReps = GetNumberOfSetsAndReps();
            double setsModifier =  (double)setsAndReps.Sets / (double)(Exercises.Count * 3);
            double repsModifier = (double)setsAndReps.Reps / (double)(Exercises.Count * 3 * 10 );

            return score * setsModifier * repsModifier;
        }

        private SetsAndReps GetNumberOfSetsAndReps()
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

    internal struct SetsAndReps
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
