using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HospitalLibrary.Core.Model
{
    [Owned]
    public class RoomFloor : ValueObject
    {
        public int Floor { get; set; }

        public RoomFloor() { }
        public RoomFloor(int floor)
        {
            if (Validation(floor))
            {
                Floor = floor;
            }
        }

        private bool Validation(int floor)
        {
            if(floor <= 1 && floor >= 10)
            {
                return false;
            }
            return true;
        }

        public bool IsEquals(int floor)
        {
            return Floor == floor;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Floor;
        }
    }
}
