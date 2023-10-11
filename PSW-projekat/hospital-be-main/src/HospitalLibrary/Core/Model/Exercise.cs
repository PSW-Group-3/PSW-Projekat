using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Exercise : ValueObject
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public double WeightLifted { get; set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
            yield return Sets;
            yield return Reps;
            yield return WeightLifted;
        }

        public Exercise() { }

        public Exercise(string name, string description, int sets, int reps, double weightLifted)
        {
            if (!IsValid()) throw new Exception("Exercise invalid");

            Name = name;
            Description = description;
            Sets = sets;
            Reps = reps;
            WeightLifted = weightLifted;
        }

        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Name))
            {
                return false;
            }   

            if (Sets < 0 || Reps < 0 || WeightLifted < 0)
            {
                return false;
            }
            return true;
        }
    }
}
