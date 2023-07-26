﻿using HospitalLibrary.Core.Model;
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
            throw new NotImplementedException();
        }

        public IEnumerable<Meal> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meal> GetAllMealsByType(MealType mealType)
        {
            return _mealRepository.GetAllMealsByType(mealType);
        }

        public Meal GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meal> GetMealsForPatient(int patientId)
        {
            return _mealRepository.GetMealsForPatient(patientId);
        }

        public void Update(Meal entity)
        {
            throw new NotImplementedException();
        }
    }
}
