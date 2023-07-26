using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IMealService : IService<Meal>
    {
        IEnumerable<Meal> GetAllMealsByType(MealType mealType);
        IEnumerable<Meal> GetMealsForPatient(int patientId);
    }
}
