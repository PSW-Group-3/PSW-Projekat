using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DTO
{
    public class GymWorkoutInfoDTO
    {
        public WorkoutType Type { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
        public List<ExerciseDTO> Exercises { get; set; }

        public GymWorkoutInfoDTO(WorkoutType type, DateTime date, int duration, string description, int personId, List<ExerciseDTO> exercises)
        {
            Type = type;
            Date = date;
            Duration = duration;
            Description = description;
            PersonId = personId;
            Exercises = exercises;
        }
    }
}
