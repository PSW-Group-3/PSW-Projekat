using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class PatientHealthInformationService : IPatientHealthInformationService
    {
        private readonly IPatientHealthInformationRepository _patientHealthInformationRepository;

        public PatientHealthInformationService(IPatientHealthInformationRepository patientHealthInformationRepository)
        {
            _patientHealthInformationRepository = patientHealthInformationRepository;
        }

        public void Create(PatientHealthInformation entity)
        {
            _patientHealthInformationRepository.Create(entity);
        }

        public void Delete(PatientHealthInformation entity)
        {
            _patientHealthInformationRepository.Delete(entity);
        }

        public IEnumerable<PatientHealthInformation> GetAll()
        {
            return _patientHealthInformationRepository.GetAll();
        }

        public PatientHealthInformation GetById(int id)
        {
            return _patientHealthInformationRepository.GetById(id);
        }

        public PatientHealthInformation GetLatestByPatientId(int id)
        {
            return _patientHealthInformationRepository.GetLatestByPatientId(id);
        }

        public void Update(PatientHealthInformation entity)
        {
            _patientHealthInformationRepository.Update(entity);
        }
    }
}
