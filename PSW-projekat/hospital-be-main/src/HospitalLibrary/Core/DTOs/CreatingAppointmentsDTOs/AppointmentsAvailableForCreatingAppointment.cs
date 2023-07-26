using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs
{
    public class AppointmentsAvailableForCreatingAppointment
    {
        public string dayInWeek { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string doctorFullName { get; set; }
        public string specialtization { get; set; }
        public int doctorID { get; set; }

        public AppointmentsAvailableForCreatingAppointment()
        {

        }

        public AppointmentsAvailableForCreatingAppointment(Appointment appointment)
        {
            this.dayInWeek = "(" + appointment.DateTime.DayOfWeek.ToString() + ")";
            this.date = appointment.DateTime.ToShortDateString();
            this.time = appointment.DateTime.ToShortTimeString();
            this.doctorFullName = appointment.Doctor.Person.Name + " " + appointment.Doctor.Person.Surname;
            this.specialtization = appointment.Doctor.Specialization.ToString();
            this.doctorID = appointment.Doctor.Id;
        }

        public AppointmentsAvailableForCreatingAppointment(
            string dayInWeek, 
            string date, 
            string time, 
            string doctorFullName, 
            string specialtization,
            int doctorID
        )
        {
            this.dayInWeek = dayInWeek;
            this.date = date;
            this.time = time;
            this.doctorFullName = doctorFullName;
            this.specialtization = specialtization;
            this.doctorID = doctorID;
        }
    }
}
