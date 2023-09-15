using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IGymWorkoutRepository : IRepository<GymWorkout>
    {
        IEnumerable<GymWorkout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate);
    }
}
