﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationAPI.DTO
{
    public class BloodBankDTO : BaseModelDTO
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [Url]
        public string ServerAddress { get; set; }

        public string ApiKey { get; set; }
    }
}
