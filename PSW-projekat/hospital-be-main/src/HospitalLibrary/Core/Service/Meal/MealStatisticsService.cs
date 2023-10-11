using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class MealStatisticsService : IMealStatisticsService
    {
        private readonly IMealRepository _mealRepository;

        public MealStatisticsService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }

        public MealsStatistics GetMealsStatistics(int patientId)
        {
            MealStatistics breakfastStatisticsDTO = GetMealStatistics(patientId, MealType.breakfast);
            MealStatistics lunchStatisticsDTO = GetMealStatistics(patientId, MealType.lunch);
            MealStatistics dinnerStatisticsDTO = GetMealStatistics(patientId, MealType.dinner);
            MealStatistics waterIntakeStatisticsDTO = GetMealStatistics(patientId, MealType.water);

            return new MealsStatistics(
                breakfastStatisticsDTO.MealScores, breakfastStatisticsDTO.MealLabels,
                lunchStatisticsDTO.MealScores, lunchStatisticsDTO.MealLabels,
                dinnerStatisticsDTO.MealScores, dinnerStatisticsDTO.MealLabels,
                waterIntakeStatisticsDTO.MealScores, waterIntakeStatisticsDTO.MealLabels
                );
        }

        public MealStatistics GetMealStatistics(int patientId, MealType mealType)
        {
            List<Meal> meals = (List<Meal>)_mealRepository.GetMealsForPatientInLast30DaysByType(patientId, mealType);
            List<float> breakfastScores = new List<float>();
            List<String> breakfastLabels = new List<String>();
            foreach (Meal meal in meals)
            {
                breakfastScores.Add(meal.Score);
                breakfastLabels.Add(meal.Date.ToShortDateString());
            }
            return new MealStatistics(breakfastScores, breakfastLabels);
        }
    }
}
