using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class MealInfoDTO
    {
        public String MealScore { get; set; }
        public MealType MealType { get; set; }
    }
}
