namespace HospitalLibrary.Core.Model.Aggregate.useCases
{
    public class ChooseDoctor
    {
        private SchedulingAppointmentEventsRepository _schedulingAppointmentEventsRepository;

        public ChooseDoctor(SchedulingAppointmentEventsRepository schedulingAppointmentEventsRepository)
        {
            _schedulingAppointmentEventsRepository = schedulingAppointmentEventsRepository;
        }

        public void Execute(int id, string doctorName)
        {
            ScheduleAppointmentByPatient scheduleAppointmentByPatient = _schedulingAppointmentEventsRepository.findById(id);

            scheduleAppointmentByPatient.ChooseDoctor(doctorName);

            _schedulingAppointmentEventsRepository.AddAppointmentTimeEvent(scheduleAppointmentByPatient);
        }
    }
}
