using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class MealStatisticsService
    {
        private readonly IMealRepository _mealRepository;

        public MealStatisticsService(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }



        public MealsStatisticsDTO GetMealStatistics(int patientId)
        {
            MealStatisticsDTO breakfastStatisticsDTO = GetBreakfastStatistics(patientId);
            MealStatisticsDTO lunchStatisticsDTO = GetLunchStatistics(patientId);
            MealStatisticsDTO dinnerStatisticsDTO = GetDinnerStatistics(patientId);
            MealStatisticsDTO waterIntakeStatisticsDTO = GetWaterIntakeStatistics(patientId);

            return new MealsStatisticsDTO(
                breakfastStatisticsDTO.MealScores, breakfastStatisticsDTO.MealLabels,
                lunchStatisticsDTO.MealScores, lunchStatisticsDTO.MealLabels,
                dinnerStatisticsDTO.MealScores, dinnerStatisticsDTO.MealLabels,
                waterIntakeStatisticsDTO.MealScores, waterIntakeStatisticsDTO.MealLabels
                );
        }

        private MealStatisticsDTO GetBreakfastStatistics(int patientId)
        {
            List<Meal> meals = (List<Meal>)_mealRepository.GetMealsForPatientInLast30DaysByType(patientId, MealType.breakfast);
            List<float> breakfastScores = new List<float>();
            List<String> breakfastLabels = new List<String>();
            foreach (Meal meal in meals)
            {
                breakfastScores.Add(meal.Score);
                breakfastLabels.Add(meal.DateOfMeal.ToShortDateString());
            }
            return new MealStatisticsDTO(breakfastScores, breakfastLabels);
        }

        private MealStatisticsDTO GetLunchStatistics(int patientId)
        {
            List<Meal> meals = (List<Meal>)_mealRepository.GetMealsForPatientInLast30DaysByType(patientId, MealType.lunch);
            List<float> lunchScores = new List<float>();
            List<String> lunchLabels = new List<String>();
            foreach (Meal meal in meals)
            {
                lunchScores.Add(meal.Score);
                lunchLabels.Add(meal.DateOfMeal.ToShortDateString());
            }
            return new MealStatisticsDTO(lunchScores, lunchLabels);
        }

        private MealStatisticsDTO GetDinnerStatistics(int patientId)
        {
            List<Meal> meals = (List<Meal>)_mealRepository.GetMealsForPatientInLast30DaysByType(patientId, MealType.dinner);
            List<float> dinnerScores = new List<float>();
            List<String> dinnerLabels = new List<String>();
            foreach (Meal meal in meals)
            {
                dinnerScores.Add(meal.Score);
                dinnerLabels.Add(meal.DateOfMeal.ToShortDateString());
            }
            return new MealStatisticsDTO(dinnerScores, dinnerLabels);
        }

        private MealStatisticsDTO GetWaterIntakeStatistics(int patientId)
        {
            List<Meal> meals = (List<Meal>)_mealRepository.GetMealsForPatientInLast30DaysByType(patientId, MealType.water);
            List<float> waterIntakeScores = new List<float>();
            List<String> waterIntakeLabels = new List<String>();
            foreach (Meal meal in meals)
            {
                waterIntakeScores.Add(meal.Score);
                waterIntakeLabels.Add(meal.DateOfMeal.ToShortDateString());
            }
            return new MealStatisticsDTO(waterIntakeScores, waterIntakeLabels);
        }

    }
}
