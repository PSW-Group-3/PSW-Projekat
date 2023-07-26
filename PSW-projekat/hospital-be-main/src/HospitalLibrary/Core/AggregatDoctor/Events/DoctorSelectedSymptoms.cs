using System;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class DoctorSelectedSymptoms : DomainEvent
    {
        public string Symptoms { get; set; }

        public DoctorSelectedSymptoms()
        {
            phase = ExaminationStage.symptomsChoosen;
            selectionTime = DateTime.Now;
        }

        public DoctorSelectedSymptoms(string symptoms)
        {
            phase = ExaminationStage.symptomsChoosen;
            Symptoms = symptoms;
            selectionTime = DateTime.Now;
        }
    }
}
