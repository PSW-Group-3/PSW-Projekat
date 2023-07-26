namespace HospitalLibrary.Core.AggregatDoctor.use
{
    public class BackToPrescriptionsChoosing
    {
        private DoctorExaminationEventsRepository _doctorExaminationEventsRepository;

        public BackToPrescriptionsChoosing(DoctorExaminationEventsRepository doctorExaminationEventsRepository)
        {
            _doctorExaminationEventsRepository = doctorExaminationEventsRepository;
        }

        public void Execute(int id)
        {
            DoctorExamination doctorExamination = _doctorExaminationEventsRepository.findById(id);

            doctorExamination.BackToPrescriptionChoosing();

            _doctorExaminationEventsRepository.AddExaminationTimeEvent(doctorExamination);
        }
    }
}
