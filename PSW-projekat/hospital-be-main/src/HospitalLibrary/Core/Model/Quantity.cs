﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    [Owned]
    public class Quantity : ValueObject
    {
        public int Value { get; }
        public Quantity() { }
        public Quantity(int value) {
            if (Validation(value)) {
                Value = value;
            }
        }

        private bool Validation(int value)
        {
            return 0 <= value;
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
        
        public bool IsEquals(int value) {
            return Value == value;
        }

        public bool IsGreater(int value) {
            return Value > value;
        }
        public Quantity Reduce(int value) {
            if (this.IsGreater(value)) {
                return new Quantity(Value-value);
            }
            return this;
        }

    }
}
