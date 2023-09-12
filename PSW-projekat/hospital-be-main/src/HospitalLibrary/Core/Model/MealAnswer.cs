using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalLibrary.Core.Model
{
    [Owned]
    public class MealAnswer : BaseModel
    {
        public virtual MealQuestion MealQuestion { get; set; }
        public float Answer { get; set; }

        public MealAnswer() { }

        public MealAnswer(MealQuestion mealQuestion, float answer)
        {
            if (!IsValid(answer)) throw new Exception("Answer invalid.");

            MealQuestion = mealQuestion;
            Answer = answer;
        }

        private bool IsValid(float answer)
        {
            if (answer < -1 || answer > 1) return false;
            return true;
        }

    }
}
