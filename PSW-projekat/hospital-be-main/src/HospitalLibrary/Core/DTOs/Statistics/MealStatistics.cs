using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class MealStatistics
    {
        public List<float> MealScores { get; }
        public List<String> MealLabels { get; }

        public MealStatistics(List<float> mealScores, List<String> mealLabels)
        {
            MealScores = mealScores;
            MealLabels = mealLabels;
        }
    }
}
