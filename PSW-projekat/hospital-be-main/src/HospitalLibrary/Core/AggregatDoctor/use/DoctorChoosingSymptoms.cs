namespace HospitalLibrary.Core.AggregatDoctor.use
{
    public class DoctorChoosingSymptoms
    {
        private DoctorExaminationEventsRepository _doctorExaminationEventsRepository;

        public DoctorChoosingSymptoms(DoctorExaminationEventsRepository doctorExaminationEventsRepository)
        {
            _doctorExaminationEventsRepository = doctorExaminationEventsRepository;
        }

        public void Execute(int id, string symptoms)
        {
            DoctorExamination doctorExamination = _doctorExaminationEventsRepository.findById(id);

            doctorExamination.ChooseSymptoms(symptoms);

            _doctorExaminationEventsRepository.AddExaminationTimeEvent(doctorExamination);
        }
    }
}
