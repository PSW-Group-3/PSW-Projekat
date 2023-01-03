using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
