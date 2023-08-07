using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class MealAnswerService : IMealAnswerService
    {
        private readonly IMealAnswerRepository _mealAnswerRepository;

        public MealAnswerService(IMealAnswerRepository mealAnswerRepository)
        {
            _mealAnswerRepository = mealAnswerRepository;
        }

        public void Create(MealAnswer entity)
        {
            _mealAnswerRepository.Create(entity);
        }

        public void Delete(MealAnswer entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MealAnswer> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MealAnswer> GetAllMealAnswersByMealType(MealType mealType)
        {
            return _mealAnswerRepository.GetAllMealAnswersByMealType(mealType);
        }

        public MealAnswer GetById(int id)
        {
            throw new NotImplementedException();
        }

        public MealAnswer GetMealAnswerForMealByQuestionId(Meal meal, int mealQuestionId)
        {
            return _mealAnswerRepository.GetMealAnswerForMealByQuestionId(meal, mealQuestionId);
        }

        public void Update(MealAnswer entity)
        {
            _mealAnswerRepository.Update(entity);
        }
    }
}
