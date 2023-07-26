using System;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class DoctorSelectedPrescriptions : DomainEvent
    {
        
        public string Prescriptions { get; set; }
        public DoctorSelectedPrescriptions()
        {
            phase = ExaminationStage.symptomsChoosen;
            selectionTime = DateTime.Now;
        }

        public DoctorSelectedPrescriptions(string prescription)
        {
            phase = ExaminationStage.symptomsChoosen;
            Prescriptions = prescription;
            selectionTime = DateTime.Now;
        }
    }
}
