﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class PatientService : IPatientService
    {
        private readonly PatientRepository _patientRepository;

        public PatientService(PatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
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
            return _patientRepository.GetAll();
        }

        public Patient GetById(int id)
        {
            return _patientRepository.GetById(id);
        }

        public Patient RegisterPatient(Patient patient)
        {
            return _patientRepository.RegisterPatient(patient);
        }

        public void Update(Patient entity)
        {
            throw new NotImplementedException();
        }
    }
}