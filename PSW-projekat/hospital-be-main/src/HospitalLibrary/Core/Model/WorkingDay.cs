﻿using System;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Model
{
    public class WorkingDay : BaseModel
    {
        public Day Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}
