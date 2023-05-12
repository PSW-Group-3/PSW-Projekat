using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DTO
{
    public class MealDTO
    {
        public List<AnswerDTO> Answers { get; set; }
        public MealType MealType { get; set; }
        public int PersonId { get; set; }
    }

    public class AnswerDTO
    {
        public int QuestionId { get; set; }
        public int Answer { get; set; }
    }
}
