using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DTO
{
    public class WorkoutDTO
    {
        public WorkoutType Type { get; set; }
        //TODO: Should patient be able to add workouts in past?
        //public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
    }
}
