using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Adapters
{
    public class MealQuestionAdapter
    {
        public static MealQuestion FromDTO(MealQuestionDTO entity)
        {
            return new MealQuestion()
            {
                Id = entity.QuestionId,
                QuestionText = entity.QuestionText,
                MealType = HospitalLibrary.Core.Model.Enums.MealType.breakfast,
                Deleted = false
            };
        }

        public static MealQuestionDTO ToDTO(MealQuestion entity)
        {
            return new MealQuestionDTO()
            {
                QuestionId = entity.Id,
                QuestionText = entity.QuestionText
            };
        }

        public static List<MealQuestionDTO> ToListDTO(List<MealQuestion> entities)
        {
            List<MealQuestionDTO> dtos = new();

            foreach (MealQuestion mealQuestion in entities)
            {
                dtos.Add(ToDTO(mealQuestion));
            }

            return dtos;
        }
    }
}
