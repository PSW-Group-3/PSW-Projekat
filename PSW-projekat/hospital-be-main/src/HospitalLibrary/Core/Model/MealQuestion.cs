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
            if (!IsValid(questionText)) throw new System.Exception("Meal question invalid!");

            QuestionText = questionText;
            MealType = mealType;
        }

        private bool IsValid(string questionText)
        {
            if (string.IsNullOrEmpty(questionText)) return false;

            return true;
        }
    }
}
