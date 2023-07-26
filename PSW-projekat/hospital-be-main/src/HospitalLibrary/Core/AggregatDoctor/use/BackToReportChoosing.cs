namespace HospitalLibrary.Core.AggregatDoctor.use
{
    public class BackToReportChoosing
    {
        private DoctorExaminationEventsRepository _doctorExaminationEventsRepository;

        public BackToReportChoosing(DoctorExaminationEventsRepository doctorExaminationEventsRepository)
        {
            _doctorExaminationEventsRepository = doctorExaminationEventsRepository;
        }

        public void Execute(int id)
        {
            DoctorExamination doctorExamination = _doctorExaminationEventsRepository.findById(id);

            doctorExamination.BackToReportChoosing();

            _doctorExaminationEventsRepository.AddExaminationTimeEvent(doctorExamination);
        }
    }
}
