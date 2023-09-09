using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IGymWorkoutRepository : IRepository<GymWorkout>
    {
        IEnumerable<GymWorkout> GetAllForPatient(int patientId);
    }
}
