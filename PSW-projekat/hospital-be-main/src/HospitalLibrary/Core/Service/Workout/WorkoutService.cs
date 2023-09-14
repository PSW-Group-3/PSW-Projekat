using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class WorkoutService : IWorkoutService
    {
        private readonly IWorkoutRepository _workoutRepository;

        public WorkoutService(IWorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }

        public void Create(Workout entity)
        {
            _workoutRepository.Create(entity);
        }

        public void Delete(Workout entity)
        {
            _workoutRepository.Delete(entity);
        }

        public IEnumerable<Workout> GetAll()
        {
            return _workoutRepository.GetAll();
        }

        public IEnumerable<Workout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate)
        {
            return _workoutRepository.GetAllForPatientInsideDateRange(patientId, fromDate, untilDate);
        }

        public Workout GetById(int id)
        {
            return _workoutRepository.GetById(id);
        }

        public void Update(Workout entity)
        {
            _workoutRepository.Update(entity);
        }
    }
}
