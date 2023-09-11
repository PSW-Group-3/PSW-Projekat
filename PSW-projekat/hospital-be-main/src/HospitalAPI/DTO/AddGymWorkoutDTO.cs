using HospitalLibrary.Core.Model.Enums;
using System.Collections.Generic;

namespace HospitalAPI.DTO
{
    public class AddGymWorkoutDTO
    {
        public WorkoutType Type { get; set; }
        //TODO: Should patient be able to add workouts in past?
        //public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
        public List<ExerciseDTO> Exercises { get; set; }
    }
}
