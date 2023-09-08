using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class PatientHealthInformation : BaseModel
    {
        public virtual Patient Patient { get; set; }
        public DateTime SelectedDate { get; set; }
        public double HealthScoreDelta { get; set; }

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
        }

        private bool IsValid(float weight, int height, string bloodPressure, int heartRate)
        {
            if (weight < 0 || height < 0 || height > 300 || heartRate < 0 || heartRate > 200) return false;

            Match match = Regex.Match(bloodPressure, @"\d{2,3}/\d{2,3}");
            if (!match.Success) return false;

            return true;
        }

        public List<String> IsWithinNormalLimits()
        {
            List<String> messages = new();

            messages.Add(CheckWeight().Message);
            messages.Add(CheckHeartRate().Message);
            messages.Add(CheckBloodPressure().Message);

            return messages;
        }

        public double CalculateBMI()
        {
            return ((double)Weight / ((double)Height / 100) / ((double)Height / 100));
        }

        private MessageAndScore CheckWeight()
        {
            double BMI = CalculateBMI();
            if (BMI < 18.5)
            {
                return new MessageAndScore("Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Underweight.\nYou should eat more and consult with a nutricionist.", -5.0);
            }
            else if (BMI >= 18.5 && BMI < 25)
            {
                return new MessageAndScore("Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Normal weight.\nKeep up the good eating habit!", 0.0);
            }
            else if (BMI >= 25 && BMI < 30)
            {
                return new MessageAndScore("Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Overweight.\nYou should eat less.", -5.0);
            }
            else if (BMI >= 30)
            {
                return new MessageAndScore("Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Obese.\nYou should eat less and consult with a nutricionist.",-10.0);
            }
            else
            {
                throw new Exception("Bad BMI");
            }
        }

        private MessageAndScore CheckHeartRate()
        {
            if (HeartRate < 40)
                return new MessageAndScore("Your heart rate is too low. Please consult a doctor.", 0.0);
            else if (40 <= HeartRate && HeartRate < 60)
                return new MessageAndScore("Your heart rate is slightly decresed. Monitor it over time.", -5.0);
            else if (60 <= HeartRate && HeartRate <= 100)
                return new MessageAndScore("Your heart rate is within the normal range.", 5.0);
            else if (101 <= HeartRate && HeartRate <= 120)
                return new MessageAndScore("Your heart rate is slightly elevated. Monitor it over time.", -5.0);
            else if (HeartRate > 120)
                return new MessageAndScore("Your heart rate is elevated. Consult a doctor for advice.", 0.0);
            else
                return new MessageAndScore("Unknown.", 0.0);
        }

        private MessageAndScore CheckBloodPressure()
        {
            int systolic = int.Parse(BloodPressure.Split('/')[0]);
            int diastolic = int.Parse(BloodPressure.Split('/')[1]);

            if (systolic < 90 && diastolic < 60)
                return new MessageAndScore("Your blood pressure is low. Please consult a doctor.", 0.0);
            else if (90 <= systolic && systolic <= 120 && 60 <= diastolic && diastolic <= 80)
                return new MessageAndScore("Your blood pressure is within the normal range.", 5.0);
            else if ((120 < systolic && systolic < 130) && (60 <= diastolic && diastolic <= 80))
                return new MessageAndScore("Your blood pressure is elevated. Consider lifestyle changes.", -5.0);
            else if ((130 <= systolic && systolic < 140) || (80 <= diastolic && diastolic < 90))
                return new MessageAndScore("You are in Hypertension Stage 1. Consult a doctor for advice.", -10.0);
            else if (systolic >= 140 || diastolic >= 90)
                return new MessageAndScore("You are in Hypertension Stage 2. Consult a doctor for evaluation.", -10.0);
            else if (systolic > 180 || diastolic > 120)
                return new MessageAndScore("Your blood pressure is dangerously high. Seek medical attention immediately.", 0.0);
            else
                return new MessageAndScore("Unknown.", 0.0);
        }

        public double CalculateHealthScoreDelta()
        {
            return CheckWeight().HealthScore + CheckHeartRate().HealthScore + CheckBloodPressure().HealthScore;
        }
    }

    internal struct MessageAndScore
    {
        public string Message { get; set; }
        public double HealthScore { get; set; }

        public MessageAndScore(string message, double healthScore)
        {
            Message = message;
            HealthScore = healthScore;
        }
    }
}
