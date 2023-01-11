using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class DoctorSelectedSymptoms : DomainEvent
    {
        public virtual ICollection<Symptom> Symptoms { get; set; }

        public DoctorSelectedSymptoms()
        {
            phase = ExaminationStage.symptomsChoosen;
            selectionTime = DateTime.Now;
        }

        public DoctorSelectedSymptoms(List<Symptom> symptoms)
        {
            phase = ExaminationStage.symptomsChoosen;
            Symptoms = symptoms;
            selectionTime = DateTime.Now;
        }
    }
}
