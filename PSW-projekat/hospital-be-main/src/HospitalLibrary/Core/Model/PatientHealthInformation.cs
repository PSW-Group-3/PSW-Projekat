using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class PatientHealthInformation
    {
        public virtual Patient Patient { get; set; }
        public DateTime SelectedDate { get; set; }

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

            messages.Add(CheckWeight());
            messages.Add(CheckHeartRate());
            messages.Add(CheckBloodPressure());

            return messages;
        }

        private String CheckWeight()
        {
            double BMI = Weight / Math.Pow((Height / 100), 2);
            if (BMI < 18.5)
            {
                return "Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Underweight.\nYou should eat more and consult with a nutricionist.";
            }
            else if (BMI >= 18.5 && BMI < 25)
            {
                return "Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Normal weight.\nKeep up the good eating habit!";
            }
            else if (BMI >= 25 && BMI < 30)
            {
                return "Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Overweight.\nYou should eat less.";
            }
            else if (BMI >= 30)
            {
                return "Your BMI (Body Mass Index) is: " + BMI + "\nWhich stands for: Obese.\nYou should eat less and consult with a nutricionist.";
            }
            else
            {
                return "Unknown";
            }
        }

        private String CheckHeartRate()
        {
            if (HeartRate < 40)
                return "Your heart rate is too low. Please consult a doctor.";
            else if (40 <= HeartRate && HeartRate < 60)
                return "Your heart rate is slightly decresed. Monitor it over time.";
            else if (60 <= HeartRate && HeartRate <= 100)
                return "Your heart rate is within the normal range.";
            else if (101 <= HeartRate && HeartRate <= 120)
                return "Your heart rate is slightly elevated. Monitor it over time.";
            else if (HeartRate > 120)
                return "Your heart rate is elevated. Consult a doctor for advice.";
            else
                return "Unknown.";
        }

        private String CheckBloodPressure()
        {
            int systolic = int.Parse(BloodPressure.Split('/')[0]);
            int diastolic = int.Parse(BloodPressure.Split('/')[1]);

            if (systolic < 90 && diastolic < 60)
                return "Your blood pressure is low. Please consult a doctor.";
            else if (90 <= systolic && systolic < 120 && 60 <= diastolic && diastolic < 80)
                return "Your blood pressure is within the normal range.";
            else if ((120 <= systolic && systolic < 130) && (60 <= diastolic && diastolic < 80))
                return "Your blood pressure is elevated. Consider lifestyle changes.";
            else if ((130 <= systolic && systolic < 140) || (80 <= diastolic && diastolic < 90))
                return "You are in Hypertension Stage 1. Consult a doctor for advice.";
            else if (systolic >= 140 || diastolic >= 90)
                return "You are in Hypertension Stage 2. Consult a doctor for evaluation.";
            else if (systolic > 180 || diastolic > 120)
                return "Your blood pressure is dangerously high. Seek medical attention immediately.";
            else
                return "Unknown.";
        }

    }
}
