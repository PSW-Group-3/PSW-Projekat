using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface IMealRepository : IRepository<Meal>
    {
        public IEnumerable<Meal> GetAllMealsByType(MealType mealType);
    }
}
