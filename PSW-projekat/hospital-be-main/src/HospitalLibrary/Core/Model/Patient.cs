using HospitalLibrary.Core.Model.Enums;
using System;

namespace HospitalLibrary.Core.Model
{
    public class Patient : BaseModel
    {
        public BloodType BloodType { get; set; }
        public double HealthScore { get; set; }
        public virtual Person Person { get; set; }
        public virtual Doctor Doctor { get; set; }

        public Patient() { }

        public Patient(BloodType bloodType, Person person, Doctor doctor)
        {
            if(!IsValid(bloodType, person, doctor)) throw new Exception("Patient invalid!");

            BloodType = bloodType;
            HealthScore = 100.0;
            Person = person;
            Doctor = doctor;
        }

        private bool IsValid(BloodType bloodType, Person person, Doctor doctor)
        {
            if (typeof(BloodType) != bloodType.GetType()) return false;
            if (person == null || doctor == null) return false;

            return true;
        }

        public void UpdateHealthScore(double score)
        {
            HealthScore += score;

            if (HealthScore >= 100) HealthScore = 100;
        }
    }
}
