using HospitalLibrary.Core.Model.Enums;
using System;
using System.Text.RegularExpressions;

namespace HospitalLibrary.Core.Model
{
    public class Person : BaseModel
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public Email Email { get; set; }
        public Address Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Role Role { get; set;}
        public Jmbg Jmbg { get; set; }

        public Person() { }

        public Person(string name, string surname, Email email, Address address, Gender gender, DateTime birthDate, Role role, Jmbg jmbg)
        {
            if (Validate(name, surname, birthDate, jmbg))
            {
                Id = 0;
                Name = name;
                Surname = surname;
                Email = email;
                Address = address;
                Gender = gender;
                BirthDate = birthDate;
                Role = role;
                Jmbg = jmbg;
            }
            else
                throw new Exception("Invalid values for person");
        }
     
        private bool Validate(String name, String surname, DateTime birthDate, Jmbg jmbg)
        {
            Regex regex = new Regex(@"([A-Z]|[Č,Ć,Ž,Š,Đ]){1}(([a-z]|[č,ć,ž,š,đ])+)");
            if (!regex.IsMatch(name) || !regex.IsMatch(surname))
                return false;

            var jmbg_year = 0;
            if (jmbg.Value[4].Equals('0'))
                jmbg_year = int.Parse("2" + jmbg.Value.Substring(4, 3));
            else
                jmbg_year = int.Parse("1" + jmbg.Value.Substring(4, 3));

            if (birthDate.Day != int.Parse(jmbg.Value.Substring(0, 2)) || birthDate.Month != int.Parse(jmbg.Value.Substring(2, 2)) || birthDate.Year != jmbg_year)
                return false;

            return true;
        }

        public String GetFullName()
        {
            return Name + " " + Surname;
        }
    }
}
