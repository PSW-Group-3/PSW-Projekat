using HospitalLibrary.Core.Model.Enums;

namespace HospitalAPI.DTO
{
    public class AddWorkoutDTO
    {
        public WorkoutType Type { get; set; }
        //TODO: Should patient be able to add workouts in past?
        //public DateTime Date { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; }
        public int PersonId { get; set; }
    }
}
