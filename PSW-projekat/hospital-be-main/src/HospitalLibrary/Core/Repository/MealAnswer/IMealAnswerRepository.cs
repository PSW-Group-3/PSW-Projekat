using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Repository
{
    public interface IMealAnswerRepository : IRepository<MealAnswer>
    {
        public IEnumerable<MealAnswer> GetAllMealAnswersByMealType(MealType mealType);
    }
}
