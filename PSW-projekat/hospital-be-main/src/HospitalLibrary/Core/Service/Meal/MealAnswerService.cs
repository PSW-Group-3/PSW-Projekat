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
            _mealAnswerRepository.Delete(entity);
        }

        public IEnumerable<MealAnswer> GetAll()
        {
            return _mealAnswerRepository.GetAll();
        }

        public MealAnswer GetById(int id)
        {
            return _mealAnswerRepository.GetById(id);
        }

        public void Update(MealAnswer entity)
        {
            _mealAnswerRepository.Update(entity);
        }
    }
}
