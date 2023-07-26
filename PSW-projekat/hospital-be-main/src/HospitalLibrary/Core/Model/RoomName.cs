using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Model
{
    [Owned]
    public class RoomName : ValueObject
    {
        public String Name { get; set;}
        public RoomName() { }
        public RoomName(String name)
        {
            if (Validation(name))
            {
                Name = name;
            }
        }
        private bool Validation(String name)
        {
            if (name.Equals("STORAGE"))
            {
                return true;
            }
            /*
            else if(!TestRegex(name)){
                return false;
            }
            */
            return true;
        }

        private bool TestRegex(String name)
        {
            //prvi broj da nije nula
            //jos dva broja
            //mala ili velika slova
            Regex regex = new Regex(@"([1-9]{1}[0-9]{2}[A-Za-z]");
            return regex.IsMatch(name);
        }

        public bool IsEquals(String name)
        {
            return Name == name;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
        }
    }
}
