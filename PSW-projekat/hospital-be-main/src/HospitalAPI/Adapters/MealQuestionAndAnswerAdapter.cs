using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System.Collections.Generic;

namespace HospitalAPI.Adapters
{
    public class MealQuestionAndAnswerAdapter
    {
        public static MealQuestion FromMealQuestionDTOtoMealQuestion(MealQuestionDTO entity)
        {
            return new MealQuestion()
            {
                Id = entity.QuestionId,
                QuestionText = entity.QuestionText,
                MealType = HospitalLibrary.Core.Model.Enums.MealType.breakfast,
                Deleted = false
            };
        }

        public static MealQuestionDTO FromMealQuestionToMealQuestionDTO(MealQuestion entity)
        {
            return new MealQuestionDTO()
            {
                QuestionId = entity.Id,
                QuestionText = entity.QuestionText
            };
        }

        public static List<MealQuestionDTO> FromMealQuestionListToMealQuestionDTOList(List<MealQuestion> entities)
        {
            List<MealQuestionDTO> dtos = new();

            foreach (MealQuestion mealQuestion in entities)
            {
                dtos.Add(FromMealQuestionToMealQuestionDTO(mealQuestion));
            }

            return dtos;
        }

        public static MealAnswer FromMealAnswerDTOtoMealAnswer(MealQuestion mealQuestion, float answer)
        {
            return new(mealQuestion, answer);
        }
    }
}
