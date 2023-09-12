using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class MealQuestionService : IMealQuestionService
    {
        private readonly IMealQuestionRepository _mealQuestionRepository;

        public MealQuestionService(IMealQuestionRepository mealQuestionRepository)
        {
            _mealQuestionRepository = mealQuestionRepository;
        }

        public void Create(MealQuestion entity)
        {
            _mealQuestionRepository.Create(entity);
        }

        public void Delete(MealQuestion entity)
        {
            _mealQuestionRepository.Delete(entity);
        }

        public IEnumerable<MealQuestion> GetAll()
        {
            return _mealQuestionRepository.GetAll();
        }

        public IEnumerable<MealQuestion> GetAllMealQuestionsByMealType(MealType mealType)
        {
            return _mealQuestionRepository.GetAllMealQuestionsByMealType(mealType);
        }

        public MealQuestion GetById(int id)
        {
            return _mealQuestionRepository.GetById(id);
        }

        public void Update(MealQuestion entity)
        {
            _mealQuestionRepository.Update(entity);
        }
    }
}
