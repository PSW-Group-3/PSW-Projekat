using HospitalLibrary.Core.AggregatDoctor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Prescription : BaseModel
    {
        public virtual ICollection<Medicine> Medicines { get; set; }
        public String Description { get; set; }
        public virtual List<DomainEvent> Changes { get; set; }

        public Prescription() { }

        public Prescription(ICollection<Medicine> medicines, string description)
        {
            Medicines = medicines;
            Description = description;
        }
    }
}
