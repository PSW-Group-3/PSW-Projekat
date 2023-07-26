using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class MealQuestionRepository : IMealQuestionRepository
    {
        private readonly HospitalDbContext _context;

        public MealQuestionRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(MealQuestion entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(MealQuestion entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<MealQuestion> GetAll()
        {
            return _context.MealQuestions;
        }

        public IEnumerable<MealQuestion> GetAllMealQuestionsByMealType(MealType mealType)
        {
            return _context.MealQuestions.Where(m => m.MealType == mealType).ToList();
        }

        public MealQuestion GetById(int id)
        {
            return _context.MealQuestions.Find(id);
        }

        public void Update(MealQuestion entity)
        {
            _context.Entry(entity).State = EntityState.Modified;

            try
            {
                _context.Update(entity);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
