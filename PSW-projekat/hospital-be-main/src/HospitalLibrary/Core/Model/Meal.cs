using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Meal : BaseModel
    {
        public int Score { get; set; }
        public MealType MealType { get; set; }

        public Meal() { }

        public Meal(List<int> answers, MealType mealType)
        {
            if (Validate(answers, mealType))
            {
                Score = CalculateScore(answers);
                MealType = mealType;
            }
            else
                throw new Exception("Meal invalid.");
        }
        
        private bool Validate(List<int> answers, MealType mealType)
        {
            foreach(int answer in answers)
            {
                if (answer < 1 || answer > 5) return false;
            }

            if (mealType is not MealType.breakfast || mealType is not MealType.lunch || mealType is not MealType.dinner) return false;

            return true;
        }

        private int CalculateScore(List<int> answers)
        {
            foreach (int answer in answers)
            {
                Score += answer;
            }

            Score /= answers.Count;

            return Score;
        }
    }
}
