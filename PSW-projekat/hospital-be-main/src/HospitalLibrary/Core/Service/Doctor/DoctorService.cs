using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalLibrary.Core.Service
{
    public class DoctorService : IDoctorService
    {

        private readonly IDoctorRepository _idoctorRepository;
        private readonly IPersonRepository _personRepository;
        private readonly AllergyRepository _allergyRepository;

        public DoctorService(IDoctorRepository doctorRepository, IPersonRepository personRepository, AllergyRepository allergyRepository)
        {
            _idoctorRepository = doctorRepository;
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

        public Doctor GetDoctorByPersonId(int personId)
        {
            return _idoctorRepository.GetDoctorByPersonId(personId);
        }

        public void Update(Doctor entity)
        {
            throw new NotImplementedException();
        }

        public AllergiesAndDoctorsForPatientRegistrationDto GetAllergiesAndDoctors()
        {
            AllergiesAndDoctorsForPatientRegistrationDto allergiesAndDoctors = new AllergiesAndDoctorsForPatientRegistrationDto();
            allergiesAndDoctors.Allergies = _allergyRepository.GetAll().ToList();
            allergiesAndDoctors.Doctors = new List<SimpleDoctorDto>();

            List<Doctor> allDoctors = _idoctorRepository.GetAllDoctorsForPatientRegistration().ToList();

            foreach (var doctorPersonalInformation in allDoctors)
            { 
                SimpleDoctorDto dto = new SimpleDoctorDto()
                {
                    Id = doctorPersonalInformation.Id,
                    FullName = doctorPersonalInformation.Person.Name + " " + doctorPersonalInformation.Person.Surname
                };
                allergiesAndDoctors.Doctors.Add(dto);
            }
            return allergiesAndDoctors;
        }

        public Person getPersonByDoctorId(int id)
        {
            return _idoctorRepository.getPersonByDoctorId(id);
        }

        public Doctor RegisterDoctor(Doctor doctor)
        {
            return _idoctorRepository.RegisterDoctor(doctor);
        }

        public IEnumerable<DoctorsCouncilDto> GetAllCouncilByDoctor(int doctorId)
        {
            IEnumerable<DoctorsCouncil> allCouncils = _idoctorRepository.GetAllCouncilByDoctor(doctorId);

            List<DoctorsCouncilDto> councilsDtos = new();

            foreach (DoctorsCouncil council in allCouncils)
            {

                DoctorsCouncilDto councilsDto = new DoctorsCouncilDto();


                councilsDto.Topic = council.Topic;
                councilsDto.Start = council.Start;
                councilsDto.Duration = council.Duration;

                ICollection<DoctorDto> doctorDtos = new List<DoctorDto>();

                foreach (var doctor in council.Doctors)
                {

                    doctorDtos.Add(new DoctorDto(doctor.Id, doctor.Person.Name, doctor.Person.Surname, doctor.Person.Email.Adress,
                                                 doctor.Person.Role));

                }

                councilsDto.Doctors = doctorDtos;
                councilsDto.Id = council.Id;


                councilsDtos.Add(councilsDto);
            }
            return councilsDtos;
        }

        public List<DoctorForCreatingAppointmentDto> GetAllDoctorsForCreatingAppointment()
        {
            List<DoctorForCreatingAppointmentDto> doctorsDtos = new List<DoctorForCreatingAppointmentDto>();
            List<Doctor> allDoctors = _idoctorRepository.GetAll().ToList();
            foreach (var doctor in allDoctors)
            {
                doctorsDtos.Add(new DoctorForCreatingAppointmentDto(doctor));
            }
            return doctorsDtos;
        }

        public IEnumerable<Doctor> GetAllBySpecialization(Specialization specialization)
        {
            return _idoctorRepository.GetAllBySpecialization(specialization);
        }
    }
}
