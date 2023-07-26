namespace HospitalLibrary.Core.AggregatDoctor.use
{
    public class DoctorChoosingPrescriptions
    {
        private DoctorExaminationEventsRepository _doctorExaminationEventsRepository;

        public DoctorChoosingPrescriptions(DoctorExaminationEventsRepository doctorExaminationEventsRepository)
        {
            _doctorExaminationEventsRepository = doctorExaminationEventsRepository;
        }

        public void Execute(int id, string prescriptions)
        {
            DoctorExamination doctorExamination = _doctorExaminationEventsRepository.findById(id);

            doctorExamination.ChoosePerscriptions(prescriptions);

            _doctorExaminationEventsRepository.AddExaminationTimeEvent(doctorExamination);
        }
    }
}
