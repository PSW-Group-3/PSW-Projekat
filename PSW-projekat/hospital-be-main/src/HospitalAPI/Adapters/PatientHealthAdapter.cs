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
        public static PatientInfoDTO ToPatientInfoDTO(Patient patient, PatientHealthInformation patientHealthInformation)
        {
            return new PatientInfoDTO
            {
                BloodPressure = patientHealthInformation.BloodPressure,
                HeartRate = patientHealthInformation.HeartRate,
                Height = patientHealthInformation.Height,
                Weight = patientHealthInformation.Weight,
                SelectedDate = patientHealthInformation.SelectedDate,
                BMI = patientHealthInformation.CalculateBMI(),
                HealthScore = patient.HealthScore,
                FullName = patient.Person.GetFullName()
            };
        }

        public static PatientHealthInformation FromPatientInfoDTO(PatientInfoDTO patientInfoDTO, Patient patient)
        {
            PatientHealthInformation patientHealthInformation = new PatientHealthInformation
            {
                BloodPressure = patientInfoDTO.BloodPressure,
                HeartRate = patientInfoDTO.HeartRate,
                Height = patientInfoDTO.Height,
                Weight = patientInfoDTO.Weight,
                SelectedDate = patientInfoDTO.SelectedDate,
                Patient = patient,
                HealthScoreDelta = 0
            };

            patientHealthInformation.HealthScoreDelta = patientHealthInformation.GetHealthScoreOfBMI();

            return patientHealthInformation;
        }
    }
}
