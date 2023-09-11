using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DTO
{
    public class WorkoutInfoDTO
    {
        public WorkoutType Type { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }

        public WorkoutInfoDTO(WorkoutType type, DateTime date, int duration, string description, int personId)
        {
            Type = type;
            Date = date;
            Duration = duration;
            Description = description;
            PersonId = personId;
        }
    }
}
