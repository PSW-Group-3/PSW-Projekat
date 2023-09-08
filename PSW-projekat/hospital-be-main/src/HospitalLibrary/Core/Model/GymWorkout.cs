using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class GymWorkout : Workout
    {
        public List<Exercise> Exercises { get; set; }

        public GymWorkout() { }

        public GymWorkout(WorkoutType type, DateTime date, TimeSpan duration, string description, List<Exercise> exercises)
        {
            if (!IsValid(date, duration)) throw new Exception("GymWorkout invalid!");

            Score = CalculateScore(type);
            Type = type;
            Date = date;
            Duration = duration;
            Description = description;
            Exercises = exercises;
        }

        protected override double CalculateScore(WorkoutType type)
        {
            double score = GetWorkoutScore(type);
            SetsAndReps setsAndReps = GetNumberOfSetsAndReps();
            double setsModifier = Exercises.Count * 3 / setsAndReps.Sets;
            double repsModifier = Exercises.Count * 3 * 10 / setsAndReps.Reps;

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
