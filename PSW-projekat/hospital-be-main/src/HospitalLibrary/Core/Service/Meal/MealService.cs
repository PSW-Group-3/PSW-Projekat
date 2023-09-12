using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class MealService : IMealService
    {
        private readonly IMealRepository _mealRepository;

        public MealService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public void Create(Meal entity)
        {
            _mealRepository.Create(entity);
        }

        public void Delete(Meal entity)
        {
            _mealRepository.Delete(entity);
        }

        public IEnumerable<Meal> GetAll()
        {
            return _mealRepository.GetAll();
        }

        public IEnumerable<Meal> GetAllMealsByType(MealType mealType)
        {
            return _mealRepository.GetAllMealsByType(mealType);
        }

        public Meal GetById(int id)
        {
            return _mealRepository.GetById(id);
        }

        public Meal GetByDateAndTypeForPatient(DateTime dateTime, MealType mealType, int patientId)
        {
            return _mealRepository.GetByDateAndTypeForPatient(dateTime, mealType, patientId);
        }

        public IEnumerable<Meal> GetAllForPatientByDate(int patientId, DateTime dateTime)
        {
            return _mealRepository.GetAllForPatientByDate(patientId, dateTime);
        }

        public void Update(Meal entity)
        {
            _mealRepository.Update(entity);
        }
    }
}
