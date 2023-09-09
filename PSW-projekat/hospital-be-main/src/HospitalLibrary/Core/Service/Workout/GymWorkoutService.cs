using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class GymWorkoutService : IGymWorkoutService
    {
        private readonly IGymWorkoutRepository _gymWorkoutRepository;

        public GymWorkoutService(IGymWorkoutRepository workoutRepository)
        {
            _gymWorkoutRepository = workoutRepository;
        }

        public void Create(GymWorkout entity)
        {
            _gymWorkoutRepository.Create(entity);
        }

        public void Delete(GymWorkout entity)
        {
            _gymWorkoutRepository.Delete(entity);
        }

        public IEnumerable<GymWorkout> GetAll()
        {
            return _gymWorkoutRepository.GetAll();
        }

        public IEnumerable<GymWorkout> GetAllForPatient(int patientId)
        {
            return _gymWorkoutRepository.GetAllForPatient(patientId);
        }

        public GymWorkout GetById(int id)
        {
            return _gymWorkoutRepository.GetById(id);
        }

        public void Update(GymWorkout entity)
        {
            _gymWorkoutRepository.Update(entity);
        }
    }
}
