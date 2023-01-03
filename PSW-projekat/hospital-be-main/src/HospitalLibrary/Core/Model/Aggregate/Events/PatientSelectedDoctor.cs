using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
