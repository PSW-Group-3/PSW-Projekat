using HospitalLibrary.Core.DomainService.Interface;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService
{
    public class MealScoreService : IMealScoreService
    {
        public MealScoreService() { }

        public float CalculateMealScore(List<MealAnswer> answers)
        {
            float score = 0;
            foreach (MealAnswer answer in answers)
            {
                score += answer.Answer;
            }

            score /= answers.Count;

            return score;
        }

        public Meal UpdateMealScore(Meal meal, List<MealAnswerDTO> dtos)
        {
            foreach (MealAnswer answer in meal.Answers)
            {
                foreach (MealAnswerDTO answerDto in dtos)
                {
                    if (answerDto.AnswerId == answer.Id)
                    {
                        answer.Answer = answerDto.Answer;
                        break;
                    }
                }
            }

            meal.Score = CalculateMealScore(meal.Answers);

            return meal;
        }
    }
}
