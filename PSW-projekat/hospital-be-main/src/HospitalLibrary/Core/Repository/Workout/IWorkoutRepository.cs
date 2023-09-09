using System.Collections.Generic;
using HospitalLibrary.Core.Model;


namespace HospitalLibrary.Core.Repository
{
    public interface IWorkoutRepository : IRepository<Workout>
    {
        IEnumerable<Workout> GetAllForPatient(int patientId);
    }
}
