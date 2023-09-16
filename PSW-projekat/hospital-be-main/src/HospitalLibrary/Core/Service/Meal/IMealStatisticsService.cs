using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IMealStatisticsService
    {
        MealsStatistics GetMealsStatistics(int patientId);
        MealStatistics GetMealStatistics(int patientId, MealType mealType);
    }
}
