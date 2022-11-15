using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Appointment: BaseModel
    {
        public virtual Person Patient { get; set; }
        public virtual Person Doctor { get; set; }
        public DateTime DateTime { get; set; }
      }
}
