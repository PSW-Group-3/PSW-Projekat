using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class WorkoutStatisticsService : IWorkoutStatisticsService
    {
        private readonly IWorkoutRepository _workoutRepository;
        private readonly IGymWorkoutRepository _gymWorkoutRepository;

        public WorkoutStatisticsService(IWorkoutRepository workoutRepository, IGymWorkoutRepository gymWorkoutRepository)
        {
            _workoutRepository = workoutRepository;
            _gymWorkoutRepository = gymWorkoutRepository;
        }

        public AllWorkoutsStatistics GetAllWorkoutsStatistics(int patientId)
        {
            AllWorkoutsStatistics workoutsStatistics = new();
            foreach (WorkoutType type in Enum.GetValues(typeof(WorkoutType)))
            {
                workoutsStatistics.WorkoutsStatistics.Add(GetWorkoutStatistics(patientId, type));
            }
            return workoutsStatistics;
        }

        public WorkoutStatistics GetWorkoutStatistics(int patientId, WorkoutType workoutType)
        {
            TimeSpan numberOfHoursSpent = new();

            if (workoutType == WorkoutType.strength)
            {   
                IEnumerable<GymWorkout> gymWorkouts = _gymWorkoutRepository.GetAllForPatientInsideDateRangeByType(patientId, DateTime.Today.AddDays(-30), DateTime.Today, workoutType);
                foreach(GymWorkout w in gymWorkouts.ToList())
                {
                    numberOfHoursSpent = numberOfHoursSpent.Add(w.Duration);
                }
                return new WorkoutStatistics(workoutType, gymWorkouts.Count(), numberOfHoursSpent.TotalHours);
            }

            IEnumerable<Workout> workouts = _workoutRepository.GetAllForPatientInsideDateRangeByType(patientId, DateTime.Today.AddDays(-30), DateTime.Today, workoutType);
            foreach (Workout w in workouts.ToList())
            {
                numberOfHoursSpent = numberOfHoursSpent.Add(w.Duration);
            }
            return new WorkoutStatistics(workoutType, workouts.Count(), numberOfHoursSpent.TotalHours);
        }
    }
}
