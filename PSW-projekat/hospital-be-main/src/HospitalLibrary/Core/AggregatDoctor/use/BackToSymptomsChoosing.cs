namespace HospitalLibrary.Core.AggregatDoctor.use
{
    public class BackToSymptomsChoosing
    {
        private DoctorExaminationEventsRepository _doctorExaminationEventsRepository;

        public BackToSymptomsChoosing(DoctorExaminationEventsRepository doctorExaminationEventsRepository)
        {
            _doctorExaminationEventsRepository = doctorExaminationEventsRepository;
        }

        public void Execute(int id)
        {
            DoctorExamination doctorExamination = _doctorExaminationEventsRepository.findById(id);

            doctorExamination.BackToSymptomsChoosing();

            _doctorExaminationEventsRepository.AddExaminationTimeEvent(doctorExamination);
        }
    }
}
