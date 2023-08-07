using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IMealRepository : IRepository<Meal>
    {
        IEnumerable<Meal> GetAllMealsByType(MealType mealType);
        IEnumerable<Meal> GetMealsForPatient(int patientId);
        Meal GetByDateAndType(DateTime dateTime, MealType mealType);
    }
}
