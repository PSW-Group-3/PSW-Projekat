namespace HospitalLibrary.Core.Model.Aggregate.useCases
{
    public class ChooseSpecialization
    {
        private SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public ChooseSpecialization(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        public void Execute(int id, string specialization)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);

            scheduleAppointmentByPatient.ChooseSpecialization(specialization);

            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(scheduleAppointmentByPatient);
        }
    }
}
