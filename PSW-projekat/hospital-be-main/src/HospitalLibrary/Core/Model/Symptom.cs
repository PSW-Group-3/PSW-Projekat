using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Model
{
    //[Owned]
    public class Symptom : BaseModel
    {
        public String Name { get; set; }

        public Symptom() { }

        public Symptom(int id, bool deleted, String name)
        {
            if (Validation(name, deleted, id))
            {
                Id = id;
                Deleted = deleted;
                Name = name;
            }
        }

        private bool Validation(String name, bool deleted, int id)
        {
            if (name.Equals(""))
            {
                return false;
            }
            if (id <= 0)
            {
                return false;
            }
            if(Deleted)
            {
                return false;
            }

            if (!TestRegex(name))
                return false;

            return true;
        }

        private bool TestRegex(String value)
        {
            //prvo veliko slovo
            //sve slova
            //minimum osam karaktera

            Regex regex = new Regex(@"([A-Z]|[Č,Ć,Ž,Š,Đ]){1}(([a-z]|[č,ć,ž,š,đ])+){8,}");
            return regex.IsMatch(value);
        }


    }
}
