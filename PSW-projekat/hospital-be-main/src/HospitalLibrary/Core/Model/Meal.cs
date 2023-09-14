using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Meal : BaseModel
    {
        public float Score { get; set; }
        public DateTime DateOfMeal { get; set; }
        public MealType MealType { get; set; }
        public virtual List<MealAnswer> Answers {get; set;}
        public virtual Patient Patient { get; set; }

        public Meal() { }

        public Meal(List<MealAnswer> answers, float score, MealType mealType, Patient patient)
        {
            if (!IsValid()) throw new Exception("Meal invalid!");

            Score = score;
            DateOfMeal = DateTime.Today;
            MealType = mealType;
            Patient = patient;
            Answers = answers;
        }
        
        private bool IsValid()
        {
            return true;
        }
    }
}
