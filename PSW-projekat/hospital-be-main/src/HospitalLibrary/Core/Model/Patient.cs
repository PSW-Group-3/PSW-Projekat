using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Patient : BaseModel
    {
        public BloodType BloodType { get; set; }
        public virtual Person Person { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
