namespace HospitalLibrary.Core.Model.Aggregate.useCases
{
    public class BackToAppointmentDateChoosing
    {
        private SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public BackToAppointmentDateChoosing(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        public void Execute(int id)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);

            scheduleAppointmentByPatient.BackToAppointmentDateChoosing();

            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(scheduleAppointmentByPatient);
        }
    }
}
