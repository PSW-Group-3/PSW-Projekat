using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Doctor : BaseModel
    {
        public Specialization Specialization { get; set; }

        public virtual Person Person { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}
