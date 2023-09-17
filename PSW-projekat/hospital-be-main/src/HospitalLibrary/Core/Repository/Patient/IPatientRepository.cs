using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IPatientRepository : IRepository<Patient>
    {
        public Patient RegisterPatient(Patient patient);
        public Person getPersonByPatientId(int id);
        IEnumerable<PatientAllergies> GetAllPatientAllergies();
        IEnumerable<int> GetAllDoctorsWhoHavePatients();
        int GetByAgeAndDoctor(DateTime dateTime1, DateTime dateTime2, int id);
        public IEnumerable<Doctor> GetAllDoctors();
        public void AddAllergyToPatient(Patient patient, List<Allergy> allergies);
        public Patient getPatientByPersonId(int id);
        public IEnumerable<Allergy> GetAllAllergiesForPatient(int id);
        public IEnumerable<int> GetAllPatientIDs();
    }
}
