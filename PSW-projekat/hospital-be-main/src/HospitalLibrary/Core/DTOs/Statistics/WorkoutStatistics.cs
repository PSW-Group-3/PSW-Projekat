using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.DTOs
{
    public class WorkoutStatistics
    {
        public WorkoutType WorkoutType { get; set; }
        public int NumberOfWorkouts { get; set; }
        public double NumberOfHoursSpent { get; set; }

        public WorkoutStatistics(WorkoutType workoutType, int numberOfWorkouts, double numberOfHoursSpent)
        {
            WorkoutType = workoutType;
            NumberOfWorkouts = numberOfWorkouts;
            NumberOfHoursSpent = numberOfHoursSpent;
        }
    }
}
