using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.DTOs
{
    public class DoctorDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Username { get; set; }
        public Role Role { get; set; }

        public DoctorDto() { }

        public DoctorDto(int id, string name, string surname, string email, Role role)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Role = role;
        }

    }
}
