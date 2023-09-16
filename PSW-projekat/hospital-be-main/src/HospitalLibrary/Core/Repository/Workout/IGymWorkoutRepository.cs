using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IGymWorkoutRepository : IRepository<GymWorkout>
    {
        IEnumerable<GymWorkout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate);
        IEnumerable<GymWorkout> GetAllForPatientInsideDateRangeByType(int patientId, DateTime fromDate, DateTime untilDate, WorkoutType workoutType);
    }
}
