using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Repository
{
    public class PatientHealthInformationRepository : IPatientHealthInformationRepository
    {
        private readonly HospitalDbContext _context;

        public PatientHealthInformationRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(PatientHealthInformation entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(PatientHealthInformation entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<PatientHealthInformation> GetAll()
        {
            return _context.PatientHealthInformations;
        }

        public PatientHealthInformation GetById(int id)
        {
            return _context.PatientHealthInformations.Find(id);
        }

        public PatientHealthInformation GetLatestByPatientId(int id)
        {
            return _context.PatientHealthInformations.Where(p => p.Patient.Id == id).OrderByDescending(p => p.SelectedDate).FirstOrDefault();
        }

        public void Update(PatientHealthInformation entity)
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
