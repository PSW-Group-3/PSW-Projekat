namespace HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs
{
    public class CheckAvailableAppontmentDto
    {
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public string fromTime { get; set; }
        public string toTime { get; set; }
        public string prefer { get; set; }
        public int selectedDoctorID { get; set; }
        public int personID { get; set; }
    }
}
