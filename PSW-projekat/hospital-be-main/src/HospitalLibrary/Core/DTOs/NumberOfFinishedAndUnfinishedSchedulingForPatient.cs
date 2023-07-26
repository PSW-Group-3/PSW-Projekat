namespace HospitalLibrary.Core.DTOs
{
    public class NumberOfFinishedAndUnfinishedSchedulingForPatient
    {
        public int PatientId { get; set; }
        public string FullName { get; set; }
        public int NumberOfFinishedSchedulings { get; set; }
        public int NumberOfUnfinishedSchedulings { get; set; }

        public NumberOfFinishedAndUnfinishedSchedulingForPatient(int patientId, string fullName, int numberOfFinishedSchedulings, int numberOfUnfinishedSchedulings)
        {
            PatientId = patientId;
            FullName = fullName;
            NumberOfFinishedSchedulings = numberOfFinishedSchedulings;
            NumberOfUnfinishedSchedulings = numberOfUnfinishedSchedulings;
        }
    }
}
