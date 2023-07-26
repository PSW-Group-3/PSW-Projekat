using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Model
{
    public class MealQuestion : BaseModel
    {
        public string QuestionText { get; set; }
        public MealType MealType { get; set; }

        public MealQuestion() { }

        public MealQuestion(string questionText, MealType mealType)
        {
            QuestionText = questionText;
            MealType = mealType;
        }
    }
}
