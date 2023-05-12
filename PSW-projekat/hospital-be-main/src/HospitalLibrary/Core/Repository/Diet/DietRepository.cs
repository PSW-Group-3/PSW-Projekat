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
    public class DietRepository : IDietRepository
    {
        private readonly HospitalDbContext _context;

        public DietRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(DailyDiet entity)
        {
            _context.Add(entity);
            _context.SaveChanges();

        }

        public void Delete(DailyDiet entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<DailyDiet> GetAll()
        {
            return _context.DailyDiets;
        }

        public DailyDiet GetById(int id)
        {
            return _context.DailyDiets.Find(id);
        }

        public void Update(DailyDiet entity)
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
