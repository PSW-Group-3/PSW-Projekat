using HospitalLibrary.Core.DomainService.Interface;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Utils
{
    [DisallowConcurrentExecution]
    public class CheckIfPatientHadAppointmentInPastXMonthsJob : IJob
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IPatientRepository _patientRepository;
        private readonly IPatientScoreService _patientScoreService;
        private readonly ILogger<CheckIfPatientHadAppointmentInPastXMonthsJob> _logger;
        private readonly int _months;

        public CheckIfPatientHadAppointmentInPastXMonthsJob(IAppointmentRepository appointmentRepository, IPatientRepository patientRepository, IPatientScoreService patientScoreService, ILogger<CheckIfPatientHadAppointmentInPastXMonthsJob> logger)
        {
            _appointmentRepository = appointmentRepository;
            _patientRepository = patientRepository;
            _patientScoreService = patientScoreService;
            _logger = logger;
            _months = 2;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            IEnumerable<int> patientIDs = _patientRepository.GetAllPatientIDs();

            foreach (int id in patientIDs)
            {
                var hadAppointment = await _appointmentRepository.CheckIfPatientHadAppointmentInPastXMonths(id, _months);

                // Log job execution details, TODO: can be removed if not necessary 
                _logger.LogInformation($"Job executed for patient ID: {id}, Had Appointment: {hadAppointment}");

                if (!hadAppointment)
                {
                    Patient patient = _patientRepository.GetById(id);
                    _patientRepository.Update(_patientScoreService.UpdatePatientHealtScore(patient, -patient.HealthScore/2));
                }
            }
        }
    }
}
