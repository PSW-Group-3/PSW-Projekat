using HospitalLibrary.Core.Model.Enums;
using System;

namespace HospitalLibrary.Core.Model
{
    public class Person : BaseModel
    {
        public String Name { get; set; }
        public String Surname { get; set; }
        public String Email { get; set; }
        public virtual Address Address { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Role Role { get; set;}
    }
}
