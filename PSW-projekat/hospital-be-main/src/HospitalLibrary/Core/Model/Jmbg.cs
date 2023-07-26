using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HospitalLibrary.Core.Model
{
    [Owned]
    public class Jmbg : ValueObject
    {

        public String Value { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public Jmbg() { }

        public Jmbg(String value)
        {
            if (Validate(value))
            {
                Value = value;
            }
            else
                throw new Exception("Invalid jmbg value");
        }

        private bool Validate(String value)
        {
            if (value.Length != 13)
                return false;
            if (!TestRegex(value))
                return false;

            var day = int.Parse(value.Substring(0, 2));
            var month = int.Parse(value.Substring(2, 2));
            var year_deprecated = value.Substring(4, 3);
            var year = 0;
            if (year_deprecated[0].Equals('0'))
                year = int.Parse("2" + year_deprecated);
            else
                year = int.Parse("1" + year_deprecated);
            try
            {
                var date = new DateTime(year, month, day);
            }
            catch(Exception e) { }

            if (!checkControlSum(getIntegerList(value)))
                return false;


            return true;
        }

        private bool TestRegex(String value)
        {
            Regex regex = new Regex(@"^\d+$");
            return regex.IsMatch(value);
        }

        private int[] getIntegerList(String value)
        {
            var list = new int[13];
            for (int i = 0; i < 13; i++)
                list[i] = int.Parse(value[i].ToString());
            return list;
        }

        private bool checkControlSum(int[] A)
        {
            var sum = 7*A[0] + 6*A[1] + 5*A[2] + 4*A[3] + 3*A[4] + 2*A[5] + 7*A[6] + 6*A[7] + 5*A[8] + 4*A[9] + 3*A[10] + 2*A[11];
            var control = sum % 11;

            if (control == 1)
                return false;

            if (control == 0)
            {
                if (A[12] != 0)
                    return false;
                else
                    return true;
            }

            if (A[12] == (11 - control))
                return true;

            return false;

        }
    }
}
