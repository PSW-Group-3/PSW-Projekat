using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;

        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Create(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.ToList();
        }

        public List<int> GetAllDoctorsForPatientRegistration()
        {
            int minPatients = GetAll().ToList().Min(pNum => pNum.Patients.Count());
            List<Doctor> allDoctors = _context.Doctors.Where(d => d.Patients.Count() <= minPatients + 2).ToList();

            List<int> doctorPersonIds = new List<int>();
            foreach (Doctor doctor in allDoctors)
            {
                doctorPersonIds.Add(doctor.Person.Id);
            }
            return doctorPersonIds;
        }

        public Doctor GetById(int id)
        {
             return _context.Doctors.Where(d => d.Id == id).FirstOrDefault();
        }

        public Person getPersonByDoctorId(int id)
        {
            var doctor = _context.Doctors.FirstOrDefault(d => d.Id == id);
            var person = _context.Persons.FirstOrDefault(d => d.Id == doctor.Person.Id);
            return person;
        }

        public Doctor RegisterDoctor(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            _context.SaveChanges();
            return doctor;
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }
    }
}
