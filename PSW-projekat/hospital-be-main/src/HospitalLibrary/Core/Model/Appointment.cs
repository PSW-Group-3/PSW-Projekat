using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Appointment: BaseModel
    {
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public DateTime DateTime { get; set; }
      }
}
