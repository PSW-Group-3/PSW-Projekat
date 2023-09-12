using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class MealInfoDTO
    {
        public String MealStatus { get; set; }
        public MealType MealType { get; set; }
        public List<MealAnswerDTO> Answers { get; set; }

        public MealInfoDTO(string mealStatus, MealType mealType, List<MealAnswerDTO> answers)
        {
            MealStatus = mealStatus;
            MealType = mealType;
            Answers = answers;
        }
    }
}
