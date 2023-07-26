using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HospitalLibrary.Core.Model
{
    [Owned]
    public class Address : ValueObject
    {
        public String Street { get; set; }
        public String Number { get; set; }
        public String City { get; set; }
        public String Township { get; set; } //opština?
        public String PostCode { get; set; }

        public Address()
        {

        }

        public Address(
            String street,
            String number,
            String city,
            String township,
            String postCode
        )
        {
            if (!ValidationStreet(street)) throw new Exception("Invalid Street. Must match: '^([A-Za-z]{3,20})$'");
            if (!ValidationNumber(number)) throw new Exception("Invalid Number. Must match: '^[1-9]{1}[0-9]{0,4}[A-Za-z]{0,2}([/]{1}[0-9]{0,4}){0,1}$'");
            if (!ValidationCity(city)) throw new Exception("Invalid City. Must match: '^([A-Za-z]{3,20})$'");
            if (!ValidationTownship(township)) throw new Exception("Invalid Township. Must match: '^([A-Za-z]{3,20})$'");
            if (!ValidationPostCode(postCode)) throw new Exception("Invalid PostCode. Must match: '^([1-9]{1}[0-9]{4})$");

            Street = street;
            Number = number;
            City = city;
            Township = township;
            PostCode = postCode;
        }

        public override string ToString()
        {
            return "Street: {" + Street + "} " +
                "Number: {" + Number + "} " +
                "City: {" + City + "} " +
                "Township: {" + Township + "} " +
                "PostCode: {" + PostCode + "} ";
        }

        private bool ValidationStreet(String street)
        {
            bool isValid = true;
            string patternStrictStreet = "^([A-Za-z]{3,20})$";
            Regex regexStreet = new Regex(patternStrictStreet);
            string[] streetParts = street.Split(' ');
            foreach (string streetPart in streetParts)
                if (!(regexStreet.IsMatch(streetPart))) isValid = false;
            return isValid;
        }

        private bool ValidationNumber(String number)
        {
            bool isValid = true;
            string patternStrictNumber = "^[1-9]{1}[0-9]{0,4}[A-Za-z]{0,2}([/]{1}[0-9]{0,4}){0,1}$";
            Regex regexNumber = new Regex(patternStrictNumber);
            if (!(regexNumber.IsMatch(number))) isValid = false;
            return isValid;
        }

        private bool ValidationCity(String city)
        {
            bool isValid = true;
            string patternStrictCity = "^([A-Za-z]{3,20})$";
            Regex regexCity = new Regex(patternStrictCity);
            string[] cityParts = city.Split(' ');
            foreach (string cityPart in cityParts)
                if (!(regexCity.IsMatch(cityPart))) isValid = false;
            return isValid;
        }

        private bool ValidationTownship(String township)
        {
            bool isValid = true;
            string patternStrictTownship = "^([A-Za-z]{3,20})$";
            Regex regexTownship = new Regex(patternStrictTownship);
            string[] townshipParts = township.Split(' ');
            foreach (string townshipPart in townshipParts)
                if (!(regexTownship.IsMatch(townshipPart))) isValid = false;
            return isValid;
        }

        private bool ValidationPostCode(String postCode)
        {
            bool isValid = true;
            string patternStrictPostCode = "^([1-9]{1}[0-9]{4})$";
            Regex regexPostCode = new Regex(patternStrictPostCode);
            if (!(regexPostCode.IsMatch(postCode))) isValid = false;
            return isValid;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street;
            yield return Number;
            yield return City;
            yield return Township;
            yield return PostCode;
        }
    }
}
