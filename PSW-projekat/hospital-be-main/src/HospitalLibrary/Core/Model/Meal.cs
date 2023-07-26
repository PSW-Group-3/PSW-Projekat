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
        public virtual Person Person { get; set; }

        public Meal() { }

        public Meal(List<MealAnswerDTO> answers, MealType mealType, Person person)
        {
            if (!IsValid(answers, mealType)) throw new Exception("Meal invalid.");
            
            Score = CalculateScore(answers);
            DateOfMeal = DateTime.Today;
            MealType = mealType;
            Person = person;        
        }
        
        private bool IsValid(List<MealAnswerDTO> answers, MealType mealType)
        {
            foreach(MealAnswerDTO answer in answers)
            {
                if (answer.Answer < -1 || answer.Answer > 1) return false;
            }

            return true;
        }

        private float CalculateScore(List<MealAnswerDTO> answers)
        {
            Score = 0;
            foreach (MealAnswerDTO answer in answers)
            {
                Score += answer.Answer;
            }

            Score /= answers.Count;

            return Score;
        }
    }
}
