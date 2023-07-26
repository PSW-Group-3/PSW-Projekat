using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Examination : BaseModel
    {
        public virtual Appointment Appointment { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Symptom> Symptoms { get; set; }
        public String Report { get; set; }

        public Examination() { }

        public Examination(Appointment appointment, ICollection<Prescription> prescriptions, ICollection<Symptom> symptoms, string report)
        {
            Appointment = appointment;
            Prescriptions = prescriptions;
            Symptoms = symptoms;
            Report = report;
        }
    }
}
