using System;

namespace HospitalLibrary.Core.Model.Aggregate.useCases
{
    public class ChooseAppointmentTime
    {
        private SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public ChooseAppointmentTime(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        public void Execute(int id, String appointmentTime)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);

            scheduleAppointmentByPatient.ChooseAppointmentTime(appointmentTime);

            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(scheduleAppointmentByPatient);
        }
    }
}
