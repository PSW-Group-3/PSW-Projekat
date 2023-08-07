using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IMealRepository : IRepository<Meal>
    {
        IEnumerable<Meal> GetAllMealsByType(MealType mealType);
        IEnumerable<Meal> GetMealsForPatientByDate(int patientId, DateTime dateTime);
        Meal GetByDateAndType(DateTime dateTime, MealType mealType);
        IEnumerable<Meal> GetMealsForPatientInLast30DaysByType(int patientId, MealType mealType);
    }
}
