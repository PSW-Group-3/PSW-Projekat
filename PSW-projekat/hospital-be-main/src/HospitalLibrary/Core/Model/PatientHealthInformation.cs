using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HospitalLibrary.Core.Model
{
    public class PatientHealthInformation : BaseModel
    {
        public virtual Patient Patient { get; set; }
        public DateTime SelectedDate { get; set; }
        public double Score { get; set; }

        public float Weight { get; set; }
        public int Height { get; set; }
        public String BloodPressure { get; set; }
        public int HeartRate { get; set; }

        public PatientHealthInformation() { }

        public PatientHealthInformation(Patient patient, float weight, int height, string bloodPressure, int heartRate)
        {
            if (!IsValid(weight, height, bloodPressure, heartRate)) throw new Exception("Health information invalid!");

            Patient = patient;
            SelectedDate = DateTime.Now;
            Weight = weight;
            Height = height;
            BloodPressure = bloodPressure;
            HeartRate = heartRate;
            Score = CalculateHealthScoreDelta();
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
            List<HealthStatus> messages = new();

            messages.Add(CheckWeight().Status);
            messages.Add(CheckHeartRate().Status);
            messages.Add(CheckBloodPressure().Status);

            return messages;
        }

        public double CalculateBMI()
        {
            return ((double)Weight / ((double)Height / 100) / ((double)Height / 100));
        }

        private StatusAndScore CheckWeight()
        {
            double BMI = CalculateBMI();
            if (BMI < 18.5)
            {
                return new StatusAndScore(HealthStatus.underweight, -5.0);
            }
            else if (BMI >= 18.5 && BMI < 25)
            {
                return new StatusAndScore(HealthStatus.normalweight, 0.0);
            }
            else if (BMI >= 25 && BMI < 30)
            {
                return new StatusAndScore(HealthStatus.overweight, -5.0);
            }
            else if (BMI >= 30)
            {
                return new StatusAndScore(HealthStatus.obese, -10.0);
            }
            else
            {
                throw new Exception("Bad BMI");
            }
        }

        private StatusAndScore CheckHeartRate()
        {
            if (HeartRate < 40)
                return new StatusAndScore(HealthStatus.criticaly_lowered_hr, 0.0);
            else if (40 <= HeartRate && HeartRate < 60)
                return new StatusAndScore(HealthStatus.slightly_lowered_hr, -5.0);
            else if (60 <= HeartRate && HeartRate <= 100)
                return new StatusAndScore(HealthStatus.normal_hr, 5.0);
            else if (101 <= HeartRate && HeartRate <= 120)
                return new StatusAndScore(HealthStatus.slightly_elevated_hr, -5.0);
            else if (HeartRate > 120)
                return new StatusAndScore(HealthStatus.criticaly_elevated_hr, 0.0);
            else
                throw new Exception("Bad hearth rate");
        }

        private StatusAndScore CheckBloodPressure()
        {
            int systolic = int.Parse(BloodPressure.Split('/')[0]);
            int diastolic = int.Parse(BloodPressure.Split('/')[1]);

            if (systolic < 90 && diastolic < 60)
                return new StatusAndScore(HealthStatus.lowered_bp, 0.0);
            else if (90 <= systolic && systolic <= 120 && 60 <= diastolic && diastolic <= 80)
                return new StatusAndScore(HealthStatus.normal_bp, 5.0);
            else if ((120 < systolic && systolic < 130) && (60 <= diastolic && diastolic <= 80))
                return new StatusAndScore(HealthStatus.elevated_bp, -5.0);
            else if ((130 <= systolic && systolic < 140) || (80 <= diastolic && diastolic < 90))
                return new StatusAndScore(HealthStatus.hypertension_1, -10.0);
            else if (systolic >= 140 || diastolic >= 90)
                return new StatusAndScore(HealthStatus.hypertension_2, -10.0);
            else if (systolic > 180 || diastolic > 120)
                return new StatusAndScore(HealthStatus.criticaly_elevated_bp, 0.0);
            else
                throw new Exception("Bad bloodpressure");
        }

        public double CalculateHealthScoreDelta()
        {
            return CheckWeight().HealthScore + CheckHeartRate().HealthScore + CheckBloodPressure().HealthScore;
        }
    }

    internal struct StatusAndScore
    {
        public HealthStatus Status { get; set; }
        public double HealthScore { get; set; }

        public StatusAndScore(HealthStatus status, double healthScore)
        {
            Status = status;
            HealthScore = healthScore;
        }
    }
}
