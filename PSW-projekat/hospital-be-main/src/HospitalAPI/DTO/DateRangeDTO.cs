using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalAPI.DTO
{
    public class DateRangeDTO
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateUntil { get; set; }
    }
}
