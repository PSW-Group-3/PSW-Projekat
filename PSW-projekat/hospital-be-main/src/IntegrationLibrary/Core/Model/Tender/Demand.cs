﻿using Microsoft.EntityFrameworkCore.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Model.Tender
{
    public class Demand : ValueObject
    {
        private BloodType _bloodType;
        private int _quantity;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public BloodType BloodType 
        { 
            get => _bloodType; 
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("Blood type is invalid");
                _bloodType = value;
            }
        }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Include)]
        public int Quantity 
        { 
            get => _quantity; 
            private set
            {
                if (value.Equals(null))
                    throw new ArgumentException("Quantity is invalid");
                _quantity = value;
            } 
        }

        public Demand() { }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return BloodType;
            yield return Quantity;
        }

        public Demand(BloodType bloodType, int quantity)
        {
            BloodType = bloodType;
            Quantity = quantity;
        }

    }
}
