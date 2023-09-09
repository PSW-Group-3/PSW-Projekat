using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class WorkoutRepository : IWorkoutRepository
    {
        private readonly HospitalDbContext _context;

        public WorkoutRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Workout entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Workout entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Workout> GetAll()
        {
            return _context.Workouts;
        }

        public IEnumerable<Workout> GetAllForPatient(int patientId)
        {
            return _context.Workouts.Where(w => w.Patient.Id == patientId);
        }

        public Workout GetById(int id)
        {
            return _context.Workouts.Find(id);
        }

        public void Update(Workout entity)
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
