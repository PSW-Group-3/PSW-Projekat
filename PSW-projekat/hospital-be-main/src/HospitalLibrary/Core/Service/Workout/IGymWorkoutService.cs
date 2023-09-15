using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IGymWorkoutService:IService<GymWorkout>
    {
        IEnumerable<GymWorkout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate);
    }
}
