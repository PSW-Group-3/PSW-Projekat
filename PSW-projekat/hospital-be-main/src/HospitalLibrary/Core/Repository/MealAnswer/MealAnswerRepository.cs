using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class MealAnswerRepository : IMealAnswerRepository
    {
        private readonly HospitalDbContext _context;

        public MealAnswerRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(MealAnswer entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(MealAnswer entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<MealAnswer> GetAll()
        {
            return _context.MealAnswers;
        }

        public IEnumerable<MealAnswer> GetAllMealAnswersByMealType(MealType mealType)
        {
            return _context.MealAnswers.Where(m => m.Meal.MealType == mealType);
        }

        public MealAnswer GetById(int id)
        {
            return _context.MealAnswers.Find(id);
        }

        public void Update(MealAnswer entity)
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
