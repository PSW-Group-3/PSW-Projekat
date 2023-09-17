using HospitalLibrary.Core.Service;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Utils
{
    public class CheckIfPatientHadAppointmentInPastXMonthsJob : IJob
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILogger<CheckIfPatientHadAppointmentInPastXMonthsJob> _logger;
        private IEnumerable<int> _patientIDs;
        private int _months;

        public CheckIfPatientHadAppointmentInPastXMonthsJob(IAppointmentService appointmentService, ILogger<CheckIfPatientHadAppointmentInPastXMonthsJob> logger)
        {
            _appointmentService = appointmentService;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.MergedJobDataMap;
            _patientIDs = (IEnumerable<int>)dataMap.Get("patientIDs");
            _months = dataMap.GetInt("months");

            foreach (int id in _patientIDs)
            {
                var hadAppointment = await _appointmentService.CheckIfPatientHadAppointmentInPastXMonths(id, _months);

                // Log job execution details
                _logger.LogInformation($"Job executed for patient ID: {id}, Had Appointment: {hadAppointment}");

                if (!hadAppointment)
                {
                    // Update score or perform other actions as needed
                }
            }
        }
    }
}
