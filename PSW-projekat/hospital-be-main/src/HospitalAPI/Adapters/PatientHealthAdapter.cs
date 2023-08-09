using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Adapters
{
    public class PatientHealthAdapter
    {
        public PatientInfoDTO ToPatientInfoDTO(Patient patient, PatientHealthInformation patientHealthInformation)
        {
            return new PatientInfoDTO
            {
                BloodPressure = patientHealthInformation.BloodPressure,
                HeartRate = patientHealthInformation.HeartRate,
                Height = patientHealthInformation.Height,
                Weight = patientHealthInformation.Weight,
                SelectedDate = patientHealthInformation.SelectedDate,

                HealthScore = patient.HealthScore,
                FullName = patient.Person.GetFullName()
            };
        }
    }
}
