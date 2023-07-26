namespace HospitalLibrary.Core.Model.Aggregate.useCases
{
    public class BackToSpecializationChoosing
    {
        private SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public BackToSpecializationChoosing(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        public void Execute(int id)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);

            scheduleAppointmentByPatient.BackToSpecializationChoosing();

            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(scheduleAppointmentByPatient);
        }
    }
}
