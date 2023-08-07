using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Service
{
    public interface IMealAnswerService : IService<MealAnswer>
    {
        IEnumerable<MealAnswer> GetAllMealAnswersByMealType(MealType mealType);
        MealAnswer GetMealAnswerForMealByQuestionId(Meal meal, int mealQuestionId);
    }
}
