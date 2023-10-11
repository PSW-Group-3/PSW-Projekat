using HospitalLibrary.Core.DomainService.Interface;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService
{
    public class PatientHealthInformationScoreService : IPatientHealthInformationScoreService
    {
        public double CalculateBloodPressureScore(string bloodPressure)
        {
            int systolic = int.Parse(bloodPressure.Split('/')[0]);
            int diastolic = int.Parse(bloodPressure.Split('/')[1]);

            if (systolic < 90 && diastolic < 60)
                return  0.0;
            else if (90 <= systolic && systolic <= 120 && 60 <= diastolic && diastolic <= 80)
                return 5.0;
            else if ((120 < systolic && systolic < 130) && (60 <= diastolic && diastolic <= 80))
                return -5.0;
            else if ((130 <= systolic && systolic < 140) || (80 <= diastolic && diastolic < 90))
                return -10.0;
            else if (systolic >= 140 || diastolic >= 90)
                return -10.0;
            else if (systolic > 180 || diastolic > 120)
                return 0.0;
            else
                throw new Exception("Bad bloodpressure");
        }

        public double CalculateBMIScore(double BMI)
        {
            if (BMI < 18.5)
            {
                return -5.0;
            }
            else if (BMI >= 18.5 && BMI < 25)
            {
                return 5.0;
            }
            else if (BMI >= 25 && BMI < 30)
            {
                return -5.0;
            }
            else if (BMI >= 30)
            {
                return -10.0;
            }
            else
            {
                throw new Exception("Bad BMI");
            }
        }

        public double CalculateHeartRateScore(int heartRate)
        {
            if (heartRate < 40)
                return 0.0;
            else if (40 <= heartRate && heartRate < 60)
                return -5.0;
            else if (60 <= heartRate && heartRate <= 100)
                return 5.0;
            else if (101 <= heartRate && heartRate <= 120)
                return -5.0;
            else if (heartRate > 120)
                return 0.0;
            else
                throw new Exception("Bad hearth rate");
        }

        public double CalculatePatientHealthInforamtionScore(PatientHealthInformation patientHealthInformation)
        {
            return CalculateBloodPressureScore(patientHealthInformation.BloodPressure) + CalculateBMIScore(patientHealthInformation.CalculateBMI()) + CalculateHeartRateScore(patientHealthInformation.HeartRate);
        }
    }
}
