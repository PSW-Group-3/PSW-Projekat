﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class WorkingDay
    {
        public int WorkingDayId { get; set; }
        public int DayOfWeek { get; set; }
        public DateTime StartTime {get; set;}
        public DateTime EndTime { get; set; }

    }
}