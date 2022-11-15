using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Service
{
    public class DoctorService : IDoctorService
    {

        private readonly DoctorRepository _doctorRepository;
        private readonly IDoctorRepository _idoctorRepository;
        private readonly PersonRepository _personRepository;
        private readonly AllergyRepository _allergyRepository;

        public DoctorService(IDoctorRepository idoctorRepository)
        {
            _idoctorRepository = idoctorRepository;

        }
        public DoctorService(DoctorRepository doctorRepository, PersonRepository personRepository, AllergyRepository allergyRepository)
        {
            _doctorRepository = doctorRepository;
            _personRepository = personRepository;
            _allergyRepository = allergyRepository;
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
            return _idoctorRepository.GetAll();
        }

        public Doctor GetById(int id)
        {
            return _idoctorRepository.GetById(id);
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }


        public AllergiesAndDoctorsForPatientRegistrationDto GetAllergiesAndDoctors()
        {
            AllergiesAndDoctorsForPatientRegistrationDto allergiesAndDoctors = new AllergiesAndDoctorsForPatientRegistrationDto();
            allergiesAndDoctors.Allergies = _allergyRepository.GetAll().ToList();
            allergiesAndDoctors.Doctors = new List<DoctorForPatientRegistrationDto>();

            List<int> allDoctorsIds = _doctorRepository.GetAllDoctorsForPatientRegistration();
            List<Person> allDoctorsPersonalInforamtion = _personRepository.GetAllDoctorsForPatientRegistration(allDoctorsIds).ToList();

            foreach (var doctorPersonalInformation in allDoctorsPersonalInforamtion)
            { 
                DoctorForPatientRegistrationDto dto = new DoctorForPatientRegistrationDto()
                {
                    Id = doctorPersonalInformation.Id,
                    FullName = doctorPersonalInformation.Name + " " + doctorPersonalInformation.Surname
                };
                allergiesAndDoctors.Doctors.Add(dto);
            }
            return allergiesAndDoctors;
        }
        

        public Person getPersonByDoctorId(int id)
        {
            return _doctorRepository.getPersonByDoctorId(id);
        }

        public Doctor RegisterDoctor(Doctor doctor)
        {
            return _doctorRepository.RegisterDoctor(doctor);
        }
        
    }
}
