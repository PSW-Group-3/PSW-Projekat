using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class MealRepository : IMealRepository
    {
        private readonly HospitalDbContext _context;

        public MealRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Meal entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Meal entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Meal> GetAll()
        {
            return _context.Meals;
        }

        public IEnumerable<Meal> GetAllMealsByType(MealType mealType)
        {
            return _context.Meals.Where(m => m.MealType == mealType);
        }

        public Meal GetById(int id)
        {
            return _context.Meals.Find(id);
        }

        public void Update(Meal entity)
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
