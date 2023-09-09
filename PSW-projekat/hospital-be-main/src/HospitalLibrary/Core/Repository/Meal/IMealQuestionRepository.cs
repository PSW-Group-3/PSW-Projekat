using System.Collections.Generic;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Repository
{
    public interface IMealQuestionRepository : IRepository<MealQuestion>
    {
        public IEnumerable<MealQuestion> GetAllMealQuestionsByMealType(MealType mealType);
    }
}
