using System;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class PatientSelectedDoctorSpecialization : DomainEvent
    {
        public PatientSelectedDoctorSpecialization() {
            phase = SchedulingStage.specChoosen;
            selectionTime = DateTime.Now;
        }

        public PatientSelectedDoctorSpecialization(string specialization) {
            phase = SchedulingStage.specChoosen;
            selectedItem = specialization;
            selectionTime = DateTime.Now;
        }
    }
}
