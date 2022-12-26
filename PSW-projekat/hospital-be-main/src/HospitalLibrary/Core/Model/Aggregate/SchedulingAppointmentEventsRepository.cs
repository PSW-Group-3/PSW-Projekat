using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static iTextSharp.text.pdf.events.IndexEvents;

namespace HospitalLibrary.Core.Model.Aggregate
{
    public class SchedulingAppointmentEventsRepository
    {
        private readonly HospitalDbContext _context;

        public SchedulingAppointmentEventsRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public ScheduleAppointmentByPatient Create(ScheduleAppointmentByPatient appointment)
        {
            _context.ScheduleAppointmentByPatients.Add(appointment);
            _context.SaveChanges();

            return appointment;
        }

        public ScheduleAppointmentByPatient findById(int id)
        {
            return _context.ScheduleAppointmentByPatients.Find(id);
        }

        public void AddAppointmentTimeEvent(ScheduleAppointmentByPatient scheduleAppointmentByPatient)
        {
            //_context.ScheduleAppointmentByPatients.Update(scheduleAppointmentByPatient);
            //_context.Entry(scheduleAppointmentByPatient);
            scheduleAppointmentByPatient.Changes[0].Aggregate = scheduleAppointmentByPatient;
            _context.AppointmentSchedulingEvents.Add(scheduleAppointmentByPatient.Changes[0]);
            _context.SaveChanges();
        }
    }
}
