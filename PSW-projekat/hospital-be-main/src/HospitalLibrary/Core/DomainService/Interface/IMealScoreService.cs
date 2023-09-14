using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DomainService.Interface
{
    public interface IMealScoreService
    {
        float CalculateMealScore(List<MealAnswer> answers);
        Meal UpdateMealScore(Meal meal, List<MealAnswerDTO> dtos);
    }
}
