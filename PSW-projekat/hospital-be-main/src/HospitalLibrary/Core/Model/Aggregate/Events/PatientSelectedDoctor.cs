using System;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class PatientSelectedDoctor : DomainEvent
    {
        public PatientSelectedDoctor() {
            phase = SchedulingStage.doctorChoosen;
            selectionTime = DateTime.Now;
        }

        public PatientSelectedDoctor(string doctorName)
        {
            phase = SchedulingStage.doctorChoosen;
            selectedItem = doctorName;
            selectionTime = DateTime.Now;
        }
    }
}
