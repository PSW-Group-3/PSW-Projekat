using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService.Interface
{
    public interface IWorkoutScoreService
    {
        double GetWorkoutScoreByType(WorkoutType type);
        double CalculateWorkoutScore(int duration, WorkoutType type);
        double CalculateGymWorkoutScore(int exercisesCount, WorkoutType type, SetsAndReps setsAndReps);
    }
}
