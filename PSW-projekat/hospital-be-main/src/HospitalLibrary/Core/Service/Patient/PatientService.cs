using System;
using System.Collections.Generic;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;

namespace HospitalLibrary.Core.Service
{
    public class PatientService : IPatientService
    {

        private readonly IPatientRepository _patientRepository;
        private readonly IBedRepository _bedRepository;


        public PatientService(IPatientRepository patientRepository, IBedRepository bedRepository)
        {
            _patientRepository = patientRepository;
            _bedRepository = bedRepository;


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

        public List<Patient> PatientsOnTreatment()
        {
            List<Patient> patientsOnTreatment = new List<Patient>();

            foreach (Bed bed in _bedRepository.GetAll())
            {
                if (!(bed.Patient == null))
                {
                    patientsOnTreatment.Add(bed.Patient);
                }
            }
            return patientsOnTreatment;
        }

        public IEnumerable<Patient> GetPatientsNoTreatment()
        {

            IEnumerable<Patient> patients = (IEnumerable<Patient>)GetAll().Except(PatientsOnTreatment());

            return patients;
        }

        public Person getPersonByPatientId(int id)
        {
            return _patientRepository.getPersonByPatientId(id);
        }

        public Patient getPatientByPersonId(int id)
        {
            return _patientRepository.getPatientByPersonId(id);
        }

        public Patient RegisterPatient(Patient patient)
        {
            return _patientRepository.RegisterPatient(patient);
        }

        public void Update(Patient entity)
        {
            _patientRepository.Update(entity);
        }

        public void AddAllergyToPatient(Patient patient, List<Allergy> allergies)
        {
            _patientRepository.AddAllergyToPatient(patient, allergies);
        }

        public IEnumerable<Allergy> GetAllAllergiesForPatient(int id)
        {
            return _patientRepository.GetAllAllergiesForPatient(id);
        }
    }
}
