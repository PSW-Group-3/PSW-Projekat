using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public class ExaminationStatisticService
    {
        private readonly DoctorExaminationEventsRepository _schedulingAppointmentEventsRepository;
        private readonly IDoctorRepository _patientRepository;

        public ExaminationStatisticService(DoctorExaminationEventsRepository schedulingAppointmentEventsRepository, IDoctorRepository patientRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
            _patientRepository = patientRepository;
        }

        public ExaminationStatisticsDTO GetAllEventStatistics()
        {
            double avarageSchedulingDuration = GetAvarageSchedulingDuration();
            List<int> numberOfLinearAndNonlinearSchedulings = GetNumberOfLinearAndNonlinearSchedulings();
            List<int> finishedSchedulingsPerDay = GetNumberOfFinishedSchedulingsPerDay();
            List<int> unfinishedSchedulingsPerDay = GetNumberOfUnfinishedSchedulingsPerDay();
            double avarageNumberOfStepsForSuccessfulScheduling = GetAvarageNumberOfStepsForSuccessfulScheduling();
            List<double> avarageNumberOfEachStepForSuccessfulScheduling = GetAvarageNumberOfEachStepForSuccessfulScheduling();
            List<NumberOfFinishedAndUnfinishedDoctorExamination> numberOfFinishedAndUnfinishedSchedulingForAllPatients = GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients();

            ExaminationStatisticsDTO dto = new ExaminationStatisticsDTO
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

        private List<NumberOfFinishedAndUnfinishedDoctorExamination> GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients()
        {
            List<NumberOfFinishedAndUnfinishedDoctorExamination> list = _schedulingAppointmentEventsRepository.GetNumberOfFinishedAndUnfinishedSchedulingForAllPatients();

            foreach (var item in list)
            {
                var person = _patientRepository.getPersonByDoctorId(item.DoctorId);
                item.FullName = person.Name + " " + person.Surname;
            }

            return list;
        }

    }
}
