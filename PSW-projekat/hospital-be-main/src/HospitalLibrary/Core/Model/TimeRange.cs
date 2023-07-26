using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    [Owned]
    public class TimeRange : ValueObject
    {
        public Time StartTime { get; }
        public Time EndTime { get; }

        public TimeRange() { }

        public TimeRange(Time startTime, Time endTime)
        {
            if(Validation(startTime, endTime))
            {
                StartTime = startTime;
                EndTime = endTime;
            }
            else throw new Exception("Invalid data - TimeRangeVO");
        }

        private bool Validation(Time startTime, Time endTime)
        {
            return startTime < endTime && endTime > startTime;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return StartTime;
            yield return EndTime;
        }

        public bool IsDuring(Time time)
        {
            return time > StartTime && time < EndTime;
        }
    }
}
