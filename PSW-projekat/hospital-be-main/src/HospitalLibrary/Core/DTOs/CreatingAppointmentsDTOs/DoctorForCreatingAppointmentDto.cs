using HospitalLibrary.Core.Model;
using System.ComponentModel.DataAnnotations;

namespace HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs
{
    public class DoctorForCreatingAppointmentDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string FullName { get; set; }
        [Required]
        public string Specialization { get; set; }

        public DoctorForCreatingAppointmentDto()
        {

        }

        public DoctorForCreatingAppointmentDto(int id, string fullName, string specialization)
        {
            Id = id;
            FullName = fullName;
            Specialization = specialization;
        }

        public DoctorForCreatingAppointmentDto(Doctor doctor)
        {
            Id = doctor.Id;
            FullName = doctor.Person.Name + " " + doctor.Person.Surname;
            Specialization = doctor.Specialization.ToString();
        }
    }
}
