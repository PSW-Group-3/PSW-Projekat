using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class GymWorkoutRepository : IGymWorkoutRepository
    {
        private readonly HospitalDbContext _context;

        public GymWorkoutRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(GymWorkout entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(GymWorkout entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<GymWorkout> GetAll()
        {
            return _context.GymWorkouts;
        }

        public IEnumerable<GymWorkout> GetAllForPatientInsideDateRange(int patientId, DateTime fromDate, DateTime untilDate)
        {
            return _context.GymWorkouts.Where(w => w.Patient.Id == patientId && w.Date.Date >= fromDate.Date && w.Date.Date <= untilDate.Date).OrderBy(w => w.Date).ToList();
        }

        public GymWorkout GetById(int id)
        {
            return _context.GymWorkouts.Find(id);
        }

        public void Update(GymWorkout entity)
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
