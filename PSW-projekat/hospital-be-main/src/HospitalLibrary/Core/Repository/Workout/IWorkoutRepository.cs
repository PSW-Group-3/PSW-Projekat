using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;


namespace HospitalLibrary.Core.Repository
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        IEnumerable<Workout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate);
    }
}
