using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IMealRepository : IRepository<Meal>
    {
        public IEnumerable<Meal> GetAllMealsByType(MealType mealType);
        public IEnumerable<Meal> GetMealsForPatient(int patientId);
    }
}
