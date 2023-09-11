namespace HospitalAPI.DTO
{
    public class ExerciseDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double WeightLifted { get; set; }

        public ExerciseDTO(string name, string description, int sets, int reps, double weightLifted)
        {
            Name = name;
            Description = description;
            Sets = sets;
            Reps = reps;
            WeightLifted = weightLifted;
        }
    }
}
