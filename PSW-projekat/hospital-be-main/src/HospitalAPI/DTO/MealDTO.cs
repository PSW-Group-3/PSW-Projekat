using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalAPI.DTO
{
    public class MealDTO
    {
        public List<MealAnswerDTO> Answers { get; set; }
        public MealType MealType { get; set; }
        public int PersonId { get; set; }
    }
}
