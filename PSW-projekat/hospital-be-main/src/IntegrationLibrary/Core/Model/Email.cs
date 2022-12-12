﻿using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model
{
    public class Email : ValueObject
    {
        public Email(string EmailAddress)
        {
            string[] EmailParts = ExtractEmailParts(EmailAddress);
            LocalPart = EmailParts[0];
            DomainName = EmailParts[1];
            ValidateCreation();
        }

        public Email()
        {
        }

        
        public string LocalPart { get; }

        public string DomainName { get; }

        [NotMapped]
        [JsonIgnore]
        public string EmailAddress 
        { 
            get 
            {
                return LocalPart + '@' + DomainName;
            } 
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return LocalPart;
            yield return DomainName;
        }

        private void ValidateCreation()
        {
            if (LocalPart != null && DomainName != null) 
            { 
                EmailAddressAttribute emailAddressAttribute = new();
                if (!emailAddressAttribute.IsValid(EmailAddress))
                {
                    throw new ArgumentException("Email isn't in the correct format");
                }
            }
            else
            {
                throw new ArgumentException("LocalPart or DomainName wasnt created");
            }
        }

        private static string[] ExtractEmailParts(string EmailAddress)
        {
            string[] EmailParts = EmailAddress.Split('@');
            if (EmailParts.Length < 2) throw new ArgumentException("Email provided doesn't contain symbol @");
            return EmailParts;
        }

    }
}
