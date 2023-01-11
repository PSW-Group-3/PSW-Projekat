﻿using System;
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
            selectionTime = DateTime.Now;
        }

        public PatientSelectedAppointmentTime(string time)
        {
            phase = SchedulingStage.timeChoosen;
            selectedItem = time;
            selectionTime = DateTime.Now;
        }
    }
}