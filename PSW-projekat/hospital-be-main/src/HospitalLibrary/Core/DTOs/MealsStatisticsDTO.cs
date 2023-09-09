using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class MealsStatisticsDTO
    {
        public List<float> BreakfastScores { get; }
        public List<String> BreakfastLabels { get; }
        public List<float> LunchScores { get; }
        public List<String> LunchLabels { get; }
        public List<float> DinnerScores { get; }
        public List<String> DinnerLabels { get; }
        public List<float> WaterIntakeScores { get; }
        public List<String> WaterIntakeLabels { get; }

        public MealsStatisticsDTO(
            List<float> breakfastScores, List<String> breakfastLabels,
            List<float> lunchScores, List<String> lunchLabels,
            List<float> dinnerScores, List<String> dinnerLabels,
            List<float> waterIntakeScores, List<String> waterIntakeLabels)
        {
            BreakfastScores = breakfastScores;
            BreakfastLabels = breakfastLabels;
            LunchScores = lunchScores;
            LunchLabels = lunchLabels;
            DinnerScores = dinnerScores;
            DinnerLabels = dinnerLabels;
            WaterIntakeScores = waterIntakeScores;
            WaterIntakeLabels = waterIntakeLabels;
        }
    }
}
