using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IMealQuestionService : IService<MealQuestion>
    {
        IEnumerable<MealQuestion> GetAllMealQuestionsByMealType(MealType mealType);
    }
}
