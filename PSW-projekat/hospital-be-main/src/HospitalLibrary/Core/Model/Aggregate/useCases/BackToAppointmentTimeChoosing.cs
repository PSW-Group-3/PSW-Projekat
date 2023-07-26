namespace HospitalLibrary.Core.Model.Aggregate.useCases
{
    public class BackToAppointmentTimeChoosing
    {
        private SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public BackToAppointmentTimeChoosing(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        public void Execute(int id)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);

            scheduleAppointmentByPatient.BackToAppointmentTimeChoosing();

            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(scheduleAppointmentByPatient);
        }
    }
}
