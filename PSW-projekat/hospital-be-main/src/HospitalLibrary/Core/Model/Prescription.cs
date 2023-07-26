using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Prescription : BaseModel
    {
        public virtual ICollection<Medicine> Medicines { get; set; }
        public String Description { get; set; }

        public Prescription() { }

        public Prescription(ICollection<Medicine> medicines, string description)
        {
            Medicines = medicines;
            Description = description;
        }
    }
}
