using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IWorkoutStatisticsService
    {
        WorkoutStatistics GetWorkoutStatistics(int patientId, WorkoutType workoutType);
        AllWorkoutsStatistics GetAllWorkoutsStatistics(int patientId);
    }
}
