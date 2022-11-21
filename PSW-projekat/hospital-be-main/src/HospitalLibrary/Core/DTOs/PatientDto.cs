﻿using HospitalLibrary.Core.Model.Enums;
using System;

namespace HospitalLibrary.Core.DTOs
{
    public class PatientDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public String Username { get; set; }
        public Role Role { get; set; }

        public PatientDto() { }

        public PatientDto(int id, string name, string surname, string email, Role role)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Role = role;
        }

    }
}
