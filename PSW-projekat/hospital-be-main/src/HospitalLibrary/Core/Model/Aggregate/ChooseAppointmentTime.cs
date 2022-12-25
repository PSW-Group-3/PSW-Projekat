using HospitalLibrary.Core.Repository;
using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class ChooseAppointmentTIme
    {
        private AppointmentRepository _appointmentRepository;
        private ISystemClock _systemClock;

        public ChooseAppointmentTIme(AppointmentRepository appointmentRepository, ISystemClock systemClock)
        {
            _appointmentRepository = appointmentRepository;
            _systemClock = systemClock;
        }

        public void Execute()
        {

        }
    }
}
