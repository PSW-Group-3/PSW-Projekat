using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IWorkoutService : IService<Workout>
    {
        IEnumerable<Workout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate);
    }
}
