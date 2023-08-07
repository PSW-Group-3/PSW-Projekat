using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.Adapters
{
    public class MealAdapter
    {
        public static MealInfoDTO ToInfoDTO(Meal entity)
        {
            String mealScore = "";
            if (entity.Score < -0.5)
            {
                mealScore = "Very Unhealthy";
            }
            else if( -0.5 <= entity.Score && entity.Score <= -0.1)
            {
                mealScore = "Unhealthy";
            }
            else if(-0.1 < entity.Score && entity.Score < 0.1)
            {
                mealScore = "Neutral";
            }
            else if(0.1 <= entity.Score && entity.Score <= 0.5)
            {
                mealScore = "Healthy";
            }
            else if(0.5 < entity.Score)
            {
                mealScore = "Very Healthy";
            }

            return new MealInfoDTO()
            {
                MealScore = mealScore,
                MealType = entity.MealType
            };
        }

        public static List<MealInfoDTO> ToListInfoDTO(List<Meal> entities)
        {
            List<MealInfoDTO> dtos = new();

            foreach (Meal meal in entities)
            {
                dtos.Add(ToInfoDTO(meal));
            }

            return dtos;
        }
    }
}
