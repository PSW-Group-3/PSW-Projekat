using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class RegisterPatientDto
    {
        [Required][MaxLength(30)]
        public String Name { get; set; }
        [Required]
        public String Surname { get; set; }
        public Gender Gender { get; set; }
        public String BirthDate { get; set; }
        public String Email { get; set; }
        public String Street { get; set; }
        public String Number { get; set; }
        public String City { get; set; }
        public String Township { get; set; }
        public String PostCode { get; set; }
        public String Username { get; set; }
        public String Password { get; set; }
        public BloodType BloodType { get; set; }   
        public List<Allergy> Allergies { get; set; }
        public DoctorForPatientRegistrationDto DoctorName { get; set; }

    }
}
