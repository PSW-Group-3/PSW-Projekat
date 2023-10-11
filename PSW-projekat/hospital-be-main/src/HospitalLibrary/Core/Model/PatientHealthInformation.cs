using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HospitalLibrary.Core.Model
{
    public class PatientHealthInformation : BaseModel
    {
        public virtual Patient Patient { get; set; }
        public DateTime Date { get; set; }
        public double Score { get; set; }

        public float Weight { get; set; }
        public int Height { get; set; }
        public String BloodPressure { get; set; }
        public int HeartRate { get; set; }

        public PatientHealthInformation() { }

        public PatientHealthInformation(Patient patient, float weight, int height, string bloodPressure, int heartRate, double score)
        {
            if (!IsValid(weight, height, bloodPressure, heartRate)) throw new Exception("Health information invalid!");

            Patient = patient;
            Date = DateTime.Now;
            Weight = weight;
            Height = height;
            BloodPressure = bloodPressure;
            HeartRate = heartRate;
            Score = score;
        }

        private bool IsValid(float weight, int height, string bloodPressure, int heartRate)
        {
            if (weight < 0 || height < 0 || height > 300 || heartRate < 0 || heartRate > 200) return false;

            Match match = Regex.Match(bloodPressure, @"\d{2,3}/\d{2,3}");
            if (!match.Success) return false;

            return true;
        }

        public List<HealthStatus> IsWithinNormalLimits()
        {
            List<HealthStatus> messages = new()
            {
                CheckWeight(),
                CheckHeartRate(),
                CheckBloodPressure()
            };

            return messages;
        }

        public double CalculateBMI()
        {
            return ((double)Weight / ((double)Height / 100) / ((double)Height / 100));
        }

        private HealthStatus CheckWeight()
        {
            double BMI = CalculateBMI();
            if (BMI < 18.5)
            {
                return HealthStatus.underweight;
            }
            else if (BMI >= 18.5 && BMI < 25)
            {
                return HealthStatus.normalweight;
            }
            else if (BMI >= 25 && BMI < 30)
            {
                return HealthStatus.overweight;
            }
            else if (BMI >= 30)
            {
                return HealthStatus.obese;
            }
            else
            {
                throw new Exception("Bad BMI");
            }
        }

        private HealthStatus CheckHeartRate()
        {
            if (HeartRate < 40)
                return HealthStatus.criticaly_lowered_hr;
            else if (40 <= HeartRate && HeartRate < 60)
                return HealthStatus.slightly_lowered_hr;
            else if (60 <= HeartRate && HeartRate <= 100)
                return HealthStatus.normal_hr;
            else if (101 <= HeartRate && HeartRate <= 120)
                return HealthStatus.slightly_elevated_hr;
            else if (HeartRate > 120)
                return HealthStatus.criticaly_elevated_hr;
            else
                throw new Exception("Bad hearth rate");
        }

        private HealthStatus CheckBloodPressure()
        {
            int systolic = int.Parse(BloodPressure.Split('/')[0]);
            int diastolic = int.Parse(BloodPressure.Split('/')[1]);

            if (systolic < 90 && diastolic < 60)
                return HealthStatus.lowered_bp;
            else if (90 <= systolic && systolic <= 120 && 60 <= diastolic && diastolic <= 80)
                return HealthStatus.normal_bp;
            else if ((120 < systolic && systolic < 130) && (60 <= diastolic && diastolic <= 80))
                return HealthStatus.elevated_bp;
            else if ((130 <= systolic && systolic < 140) || (80 <= diastolic && diastolic < 90))
                return HealthStatus.hypertension_1;
            else if (systolic >= 140 || diastolic >= 90)
                return HealthStatus.hypertension_2;
            else if (systolic > 180 || diastolic > 120)
                return HealthStatus.criticaly_elevated_bp;
            else
                throw new Exception("Bad bloodpressure");
        }

    }
}
