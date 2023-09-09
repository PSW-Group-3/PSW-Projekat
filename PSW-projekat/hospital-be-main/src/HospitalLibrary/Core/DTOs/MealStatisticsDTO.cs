using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class MealStatisticsDTO
    {
        public List<float> MealScores { get; }
        public List<String> MealLabels { get; }

        public MealStatisticsDTO(List<float> mealScores, List<String> mealLabels)
        {
            MealScores = mealScores;
            MealLabels = mealLabels;
        }
    }
}
