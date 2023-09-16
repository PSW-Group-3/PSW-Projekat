using System.Collections.Generic;

namespace HospitalLibrary.Core.DTOs
{
    public class AllWorkoutsStatistics
    {
        public List<WorkoutStatistics> WorkoutsStatistics { get; set; }

        public AllWorkoutsStatistics()
        {
            WorkoutsStatistics = new();
        }
    }
}
