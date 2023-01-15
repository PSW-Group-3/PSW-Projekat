using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Repository.CouncilOfDoctors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service.CouncilOfDoctors
{
    public class CouncilOfDoctorsService : ICouncilOfDoctorsService
    {
        private readonly ICouncilOfDoctorsRepository _councilOfDoctorsRepository;
        private readonly IDoctorRepository _idoctorRepository;
        private readonly IAppointmentRepository _appointmentRepository;
        public IWorkingDayRepository _workingDayRepository; 

        public CouncilOfDoctorsService(ICouncilOfDoctorsRepository councilOfDoctorsRepository, IDoctorRepository idoctorRepository, IAppointmentRepository appointmentRepository, IWorkingDayRepository workingDayRepository)
        {
            _appointmentRepository = appointmentRepository;
            _idoctorRepository = idoctorRepository;
            _councilOfDoctorsRepository = councilOfDoctorsRepository;
            _workingDayRepository = workingDayRepository;

        }


        public void Create(DoctorsCouncil entity)
        {
            ICollection<Doctor> doctors = new List<Doctor>();
            foreach (Doctor d in entity.Doctors) {            
                doctors.Add(_idoctorRepository.GetById(d.Id));
            }
            if (doctors.Count() > 0)
            {
                entity.Doctors = doctors;

                var ns = _appointmentRepository.GetAllFree(doctors, entity.Start, entity.End);
                if (ns.Count() > 0)
                {
                    foreach (DateTime d in ns)
                    {
                        var asdf = this.DoctorsWork(doctors, d);
                        if (asdf)
                        {
                            entity.Start = ns.First();
                            _councilOfDoctorsRepository.Create(entity);
                            return;
                        }
                    }


                }
            }



            if (entity.Specializations.Count > 0) {
                doctors = new List<Doctor>();
                var p = entity.Specializations.ToList();
                foreach (var specialization in p)
                {
                    foreach (Doctor d in _idoctorRepository.GetAllDoctorsBySpecialization(specialization))
                    {
                        doctors.Add(_idoctorRepository.GetById(d.Id));
                    }
                }
                entity.Doctors = doctors;
            }

            ICollection<Doctor> newDoctors = new List<Doctor>();
            newDoctors.Add(_idoctorRepository.GetById(1));

            foreach (Doctor d in doctors)
            {
                var tempDoctors = newDoctors;
                tempDoctors.Add(d);
                var ns = _appointmentRepository.GetAllFree(tempDoctors, entity.Start, entity.End);
                if (ns.Count() > 0) 
                {
                    newDoctors.Add(d);
                }
            }

                     
            var datas = _appointmentRepository.GetAllFree(newDoctors, entity.Start, entity.End);
            if (datas.Count() > 0)
            {
                foreach (DateTime d in datas)
                {
                    var asdf = this.IsDoctorsWork(doctors, d);
                    if (!IsAllSpecialization(entity.Specializations.Count(), asdf))
                    {
                        return;
                    }
                    entity.Doctors = asdf;
                    entity.Start = datas.First();
                    _councilOfDoctorsRepository.Create(entity);
                    return;

                }
            }
            else {
                throw new Exception();
            }
        }

        public bool IsAllSpecialization(int count, ICollection<Doctor> doctors) {
            List<Specialization> specialization = new List<Specialization>();
            foreach (Doctor d in doctors) {
                if (!specialization.Contains(d.Specialization)) {
                    specialization.Add(d.Specialization);
                }
            }

            if (specialization.Count() == count) {
                return true;
            }
            return false;
        }

        public bool DoctorsWork(ICollection<Doctor> doctors, DateTime date)
        {
            foreach (Doctor d in doctors)
            {
                var isWork = InWorkingTime(date, _workingDayRepository.GetAllWorkingDaysByUser(d.Person.Id));
                if (!isWork) {
                    return false;
                }
            }
            return true;
        }


        public ICollection<Doctor> IsDoctorsWork(ICollection<Doctor> doctors, DateTime date)
        {
            ICollection<Doctor> docs = new List<Doctor>();
            foreach (Doctor d in doctors)
            {
                var isWork = InWorkingTime(date, _workingDayRepository.GetAllWorkingDaysByUser(d.Person.Id));
                if (isWork)
                {
                    docs.Add(d);
                }
            }
            return docs;
        }

        public bool InWorkingTime(DateTime entity, IEnumerable<WorkingDay> workingDays)
        {

            foreach (WorkingDay workingDay in workingDays)
            {
                DayOfWeek dayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), workingDay.Day.ToString());

                DateTime wStart = new DateTime(1, 1, 1, workingDay.StartTime.Hour, workingDay.StartTime.Minute, workingDay.StartTime.Second);
                DateTime wEnd = new DateTime(1, 1, 1, workingDay.EndTime.Hour, workingDay.EndTime.Minute, workingDay.EndTime.Second);
                DateTime aTime = new DateTime(1, 1, 1, entity.Hour, entity.Minute, entity.Second);

                if ((dayOfWeek.Equals(entity.DayOfWeek)) && wStart <= aTime && wEnd >= aTime)
                {
                    return true;
                }
            }
            return false;
        }

        public void Delete(DoctorsCouncil entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DoctorsCouncil> GetAll()
        {
            return _councilOfDoctorsRepository.GetAll();
        }

        public DoctorsCouncil GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(DoctorsCouncil entity)
        {
            throw new NotImplementedException();
        }

       
    }
}
