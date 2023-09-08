using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IWorkoutService : IService<Workout>
    {
        IEnumerable<Workout> GetAllForPatient(int patientId);
    }
}
