using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Meal : BaseModel
    {
        public float Score { get; set; }
        public MealType MealType { get; set; }
        public virtual Person Person { get; set; }

        public Meal() { }

        public Meal(List<float> answers, MealType mealType, Person person)
        {
            if (!IsValidate(answers, mealType)) throw new Exception("Meal invalid.");
            
            Score = CalculateScore(answers);
            MealType = mealType;
            Person = person;        
        }
        
        private bool IsValidate(List<float> answers, MealType mealType)
        {
            foreach(float answer in answers)
            {
                if (answer < -1 || answer > 1) return false;
            }

            return true;
        }

        private float CalculateScore(List<float> answers)
        {
            Score = 0;
            foreach (float answer in answers)
            {
                Score += answer;
            }

            Score /= answers.Count;

            return Score;
        }
    }
}
