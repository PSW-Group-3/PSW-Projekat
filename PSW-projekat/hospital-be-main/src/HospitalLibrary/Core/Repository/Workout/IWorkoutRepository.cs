using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Repository
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        IEnumerable<Workout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate);
        IEnumerable<Workout> GetAllForPatientInsideDateRangeByType(int patientId, DateTime fromDate, DateTime untilDate, WorkoutType workoutType);
    }
}
