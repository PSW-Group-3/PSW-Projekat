using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IGymWorkoutService:IService<GymWorkout>
    {
        IEnumerable<GymWorkout> GetAllForPatient(int patientId);
    }
}
