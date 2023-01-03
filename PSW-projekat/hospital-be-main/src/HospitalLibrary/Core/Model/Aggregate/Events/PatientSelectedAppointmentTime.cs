using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate.Events
{
    public class PatientSelectedAppointmentTime : DomainEvent
    {
        public PatientSelectedAppointmentTime() {
            phase = SchedulingStage.timeChoosen;
        }

        public PatientSelectedAppointmentTime(string dateTime)
        {
            phase = SchedulingStage.timeChoosen;
            selectedItem = dateTime;
        }
    }
}
