using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;

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
            throw new NotImplementedException();
        }

        public void Delete(Patient entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.Patients.ToList();
        }

        public Patient GetById(int id)
        {
            return _context.Patients.Where(d => d.Id == id).FirstOrDefault();
        }

        public void Update(Patient entity)
        {
            throw new NotImplementedException();
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
    }
}
