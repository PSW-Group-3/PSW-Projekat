using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;

namespace HospitalAPI.Adapters
{
    public class MealAdapter
    {
        public static List<MealAnswerDTO> FromMealAnswerListToMealAnswerDTOList(List<MealAnswer> answers)
        {
            List<MealAnswerDTO> dtos = new();
            foreach(MealAnswer answer in answers)
            {
                dtos.Add(new(answer.MealQuestion.Id, answer.Id, answer.Answer));
            }
            return dtos;
        }

        public static MealInfoDTO FromMealToMealInfoDTO(Meal entity)
        {
            String mealScore = "";
            if (entity.Score < -0.5)
            {
                mealScore = "Very Unhealthy";
            }
            else if (-0.5 <= entity.Score && entity.Score <= -0.1)
            {
                mealScore = "Unhealthy";
            }
            else if (-0.1 < entity.Score && entity.Score < 0.1)
            {
                mealScore = "Neutral";
            }
            else if (0.1 <= entity.Score && entity.Score <= 0.5)
            {
                mealScore = "Healthy";
            }
            else if (0.5 < entity.Score)
            {
                mealScore = "Very Healthy";
            }

            return new MealInfoDTO(mealScore, entity.MealType, FromMealAnswerListToMealAnswerDTOList(entity.Answers));
        }

        public static List<MealInfoDTO> FromMealListToMealInfoDTOList(List<Meal> entities)
        {
            List<MealInfoDTO> dtos = new();

            foreach (Meal meal in entities)
            {
                dtos.Add(FromMealToMealInfoDTO(meal));
            }

            return dtos;
        }
    }
}
