using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        public IEnumerable<Doctor> GetAllDoctorsForPatientRegistration();
        public Person getPersonByDoctorId(int id);
        Doctor RegisterDoctor(Doctor doctor);
        public Doctor GetDoctorByPersonId(int personId);
        IEnumerable<DoctorsCouncil> GetAllCouncilByDoctor(int doctorId);
        IEnumerable<Doctor> GetAllDoctorsBySpecialization(Specialization specialization);
        public IEnumerable<Doctor> GetAllBySpecialization(Specialization specialization);
    }
}
