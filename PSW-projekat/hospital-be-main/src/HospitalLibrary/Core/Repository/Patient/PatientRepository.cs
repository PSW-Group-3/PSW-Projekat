using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;

        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Patient entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Patient entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return _context.Patients.Select(p => p.Doctor).Distinct().ToList();
        }

        public IEnumerable<Doctor> GetAllDoctors2()
        {
            return _context.Patients.Select(p => p.Doctor);
        }

        public Patient GetById(int id)
        {
            return _context.Patients.Where(d => d.Id == id).FirstOrDefault();
        }

        public void Update(Patient entity)
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

        public Patient RegisterPatient(Patient patient)
        {
            _context.Patients.Add(patient);
            _context.SaveChanges();

            return patient;
        }
        
        public Person getPersonByPatientId(int id)
        {
            var patient = _context.Patients.FirstOrDefault(d => d.Id == id);
            var person = _context.Persons.FirstOrDefault(d => d.Id == patient.Person.Id);
            return person;
        }

        public IEnumerable<int> GetAllDoctorsWhoHavePatients()
        {
            return _context.Patients.Select(o => o.Doctor.Id).Distinct().ToList();
        }

        public int GetByAgeAndDoctor(DateTime to, DateTime from, int dId)
        {
            return _context.Patients.Where(x => x.Person.BirthDate >= from && x.Person.BirthDate < to && x.Doctor.Id == dId).Count();
        }

        public IEnumerable<PatientAllergies> GetAllPatientAllergies()
        {
            return _context.PatientAllergies.ToList();
        }

        public void AddAllergyToPatient(Patient patient, List<Allergy> allergies)
        {
            foreach (Allergy allergy in allergies)
            {
                PatientAllergies patientAllergies = new PatientAllergies()
                {
                    PatientId = patient.Id,
                    AllergyId = allergy.Id,
                    Deleted = false
                };
                _context.PatientAllergies.Add(patientAllergies);
                _context.SaveChanges();
            }
        }

        public Patient getPatientByPersonId(int id)
        {
            var patient = _context.Patients.FirstOrDefault(d => d.Person.Id == id);
            return patient;
        }

        public IEnumerable<Allergy> GetAllAllergiesForPatient(int id)
        {
            var allergiesIds = _context.PatientAllergies.Where(p => p.PatientId == id).Select(a => a.AllergyId);
            var allergies = _context.Allergies.Where(a => allergiesIds.Contains(a.Id));
            return allergies;
        }
    }
}
