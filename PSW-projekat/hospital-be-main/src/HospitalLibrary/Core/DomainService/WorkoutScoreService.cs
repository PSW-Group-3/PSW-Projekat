using HospitalLibrary.Core.DomainService.Interface;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService
{
    public class WorkoutScoreService : IWorkoutScoreService
    {
        public WorkoutScoreService() { }

        public double CalculateGymWorkoutScore(int exercisesCount, WorkoutType type, SetsAndReps setsAndReps)
        {
            double score = GetWorkoutScoreByType(type);
            double setsModifier = (double)setsAndReps.Sets / (double)(exercisesCount * 3);
            double repsModifier = (double)setsAndReps.Reps / (double)(exercisesCount * 30);

            return score * setsModifier * repsModifier;
        }

        public double CalculateWorkoutScore(int durationInt, WorkoutType type)
        {
            double score = GetWorkoutScoreByType(type);
            TimeSpan duration = TimeSpan.FromMinutes(durationInt);

            if (duration.TotalMinutes > 60)
            {
                score *= duration.TotalMinutes / 60;
            }

            return score;
        }

        public double GetWorkoutScoreByType(WorkoutType type)
        {
            double score = type switch
            {
                WorkoutType.walking => 1.0,
                WorkoutType.jogging => 2.0,
                WorkoutType.swimming => 2.0,
                WorkoutType.sports => 2.0,
                WorkoutType.cardio => 3.0,
                WorkoutType.running => 3.0,
                WorkoutType.cycling => 3.0,
                WorkoutType.strength => 3.0,
                _ => 0,
            };

            return score;
        }
    }
}
