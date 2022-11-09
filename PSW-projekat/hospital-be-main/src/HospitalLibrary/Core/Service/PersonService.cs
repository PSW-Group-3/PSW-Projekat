﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;

namespace HospitalLibrary.Core.Service
{
    public class PersonService : IPersonService
    {
        private readonly AllergyRepository _allergyRepository;
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
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
            return _personRepository.GetAll();
        }

        public IEnumerable<Person> GetAllDoctors()
        {
            return _personRepository.GetAllDoctors();
        }

        public IEnumerable<Person> GetAllPatients()
        {
            return _personRepository.GetAllPatients();
        }

        public Person GetById(int id)
        {
            return _personRepository.GetById(id);
        }

        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }

        public AllergiesAndDoctorsForPatientRegistrationDto GetAllergiesAndDoctors()
        {
            IEnumerable<Person> allDoctors = _personRepository.GetAllDoctors();
            IEnumerable<Patient> allPatients = _personRepository.GetAllPatients();
            IEnumerable<Allergy> allAllergies = _allergyRepository.GetAll();

            int minPatientCount = 
        }
    }
}
