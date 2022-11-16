using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly HospitalDbContext _context;

        public PersonRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons.ToList();
        }

        public IEnumerable<Person> GetAllDoctors()
        {
            return _context.Persons.Where(x => x.Role == Role.doctor);
        }

        public IEnumerable<Person> GetAllDoctorsForPatientRegistration(List<int> doctorIds)
        {
            return _context.Persons.Where(x => x.Role == Role.doctor && doctorIds.Contains(x.Id));
        }

        public IEnumerable<Person> GetAllPatients()
        {
            return _context.Persons.Where(x => x.Role == Role.patient);
        }

        public Person GetById(int id)
        {
            return _context.Persons.Find(id);
        }

        public Doctor GetDoctorById(int id)
        {
            Doctor doctor = _context.Doctors.Where(d => d.Id == id).FirstOrDefault();
            return doctor;
        }

        public Person RegisterUser(Person user)
        {
            _context.Persons.Add(user);
            _context.SaveChanges();

            return user;
        }

        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
