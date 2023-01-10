using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class SchedulingStatisticsService
    {
        private readonly SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;
        private readonly IPatientRepository _patientRepository;

        public SchedulingStatisticsService(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository, IPatientRepository patientRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
            _patientRepository = patientRepository;
        }

        public SchedulingStatisticsDTO GetAllEventStatistics()
        {
            double avarageSchedulingDuration = GetAvarageSchedulingDuration();
            List<int> numberOfLinearAndNonlinearSchedulings = GetNumberOfLinearAndNonlinearSchedulings();
            List<int> finishedSchedulingsPerDay = GetNumberOfFinishedSchedulingsPerDay();
            List<int> unfinishedSchedulingsPerDay = GetNumberOfUnfinishedSchedulingsPerDay();
            double avarageNumberOfStepsForSuccessfulScheduling = GetAvarageNumberOfStepsForSuccessfulScheduling();
            List<double> avarageNumberOfEachStepForSuccessfulScheduling = GetAvarageNumberOfEachStepForSuccessfulScheduling();
            List<NumberOfFinishedAndUnfinishedSchedulingForPatient> numberOfFinishedAndUnfinishedSchedulingForAllPatients = GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients();

            SchedulingStatisticsDTO dto = new SchedulingStatisticsDTO
                (
                    avarageSchedulingDuration,
                    numberOfLinearAndNonlinearSchedulings[0],
                    numberOfLinearAndNonlinearSchedulings[1],
                    finishedSchedulingsPerDay,
                    unfinishedSchedulingsPerDay,
                    avarageNumberOfStepsForSuccessfulScheduling,
                    avarageNumberOfEachStepForSuccessfulScheduling,
                    numberOfFinishedAndUnfinishedSchedulingForAllPatients
                );

            return dto;
        } 

        private double GetAvarageSchedulingDuration()
        {
            return _schedulingAppointmentEventsRepository.GetAvarageSchedulingDuration();
        }

        private List<int> GetNumberOfLinearAndNonlinearSchedulings()
        {
            return _schedulingAppointmentEventsRepository.GetNumberOfLinearAndNonlinearSchedulings();
        }

        private List<int> GetNumberOfFinishedSchedulingsPerDay()
        {
            return _schedulingAppointmentEventsRepository.GetNumberOfFinishedSchedulingsPerDay();
        }

        private List<int> GetNumberOfUnfinishedSchedulingsPerDay()
        {
            return _schedulingAppointmentEventsRepository.GetNumberOfUnfinishedSchedulingsPerDay();
        }

        private double GetAvarageNumberOfStepsForSuccessfulScheduling()
        {
            return _schedulingAppointmentEventsRepository.GetAvarageNumberOfStepsForSuccessfulScheduling();
        }
        private List<double> GetAvarageNumberOfEachStepForSuccessfulScheduling()
        {
            return _schedulingAppointmentEventsRepository.GetAvarageNumberOfEachStepForSuccessfulScheduling();
        }

        private List<NumberOfFinishedAndUnfinishedSchedulingForPatient> GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients()
        {
            List<NumberOfFinishedAndUnfinishedSchedulingForPatient> list = _schedulingAppointmentEventsRepository.GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients();

            foreach(var item in list)
            {
                var person = _patientRepository.getPersonByPatientId(item.PatientId);
                item.FullName = person.Name + " " + person.Surname;
            }

            return list;
        }

    }
}
