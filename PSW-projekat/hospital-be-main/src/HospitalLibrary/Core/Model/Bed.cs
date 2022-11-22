﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Model
{
    public class Bed: BaseModel
    {
        public string Name { get; set; }
        public BedState BedState { get; set; }
        public virtual Patient Patient { get; set; }
        public int Quantity { get; set; }


    }
}
