using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IMealService : IService<Meal>
    {
        IEnumerable<Meal> GetAllMealsByType(MealType mealType);
        IEnumerable<Meal> GetMealsForPatientByDate(int patientId, DateTime dateTime);
        Meal GetByDateAndType(DateTime today, MealType mealType);
    }
}
