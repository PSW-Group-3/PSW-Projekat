using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class PatientAllergies : BaseModel
    {
        public virtual Patient Patient { get; set; }
        public virtual Allergy Allergy { get; set; }
    }
}
