﻿using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;

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

        public static PatientInfoDTO ToPatientInfoDTO(Patient patient)
        {
            return new PatientInfoDTO
            {
                BloodPressure = "",
                HeartRate = 0,
                Height = 0,
                Weight = 0,
                SelectedDate = new System.DateTime(),
                BMI = 0.0,
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
                Score = 0
            };

            patientHealthInformation.Score = patientHealthInformation.CalculateHealthScoreDelta();

            return patientHealthInformation;
        }
    }
}
