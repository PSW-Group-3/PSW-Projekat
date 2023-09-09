using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public class MealStatisticsService
    {
        private readonly IMealRepository _mealRepository;

        public MealStatisticsService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }



        public MealsStatisticsDTO GetMealsStatistics(int patientId)
        {
            MealStatisticsDTO breakfastStatisticsDTO = GetMealStatistics(patientId, MealType.breakfast);
            MealStatisticsDTO lunchStatisticsDTO = GetMealStatistics(patientId, MealType.lunch);
            MealStatisticsDTO dinnerStatisticsDTO = GetMealStatistics(patientId, MealType.dinner);
            MealStatisticsDTO waterIntakeStatisticsDTO = GetMealStatistics(patientId, MealType.water);

            return new MealsStatisticsDTO(
                breakfastStatisticsDTO.MealScores, breakfastStatisticsDTO.MealLabels,
                lunchStatisticsDTO.MealScores, lunchStatisticsDTO.MealLabels,
                dinnerStatisticsDTO.MealScores, dinnerStatisticsDTO.MealLabels,
                waterIntakeStatisticsDTO.MealScores, waterIntakeStatisticsDTO.MealLabels
                );
        }

        private MealStatisticsDTO GetMealStatistics(int patientId, MealType mealType)
        {
            List<Meal> meals = (List<Meal>)_mealRepository.GetMealsForPatientInLast30DaysByType(patientId, mealType);
            List<float> breakfastScores = new List<float>();
            List<String> breakfastLabels = new List<String>();
            foreach (Meal meal in meals)
            {
                breakfastScores.Add(meal.Score);
                breakfastLabels.Add(meal.DateOfMeal.ToShortDateString());
            }
            return new MealStatisticsDTO(breakfastScores, breakfastLabels);
        }

        

    }
}
