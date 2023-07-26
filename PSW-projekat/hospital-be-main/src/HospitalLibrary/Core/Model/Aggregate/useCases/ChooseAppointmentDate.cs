using System;

namespace HospitalLibrary.Core.Model.Aggregate.useCases
{
    public class ChooseAppointmentDate
    {
        private SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public ChooseAppointmentDate(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        public void Execute(int id, String appointmentDate)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);

            scheduleAppointmentByPatient.ChooseAppointmentDate(appointmentDate);

            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(scheduleAppointmentByPatient);
        }
    }
}
