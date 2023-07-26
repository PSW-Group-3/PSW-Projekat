using System;

namespace HospitalLibrary.Core.AggregatDoctor.use
{
    public class DoctorChoosingReport
    {
        private DoctorExaminationEventsRepository _doctorExaminationEventsRepository;

        public DoctorChoosingReport(DoctorExaminationEventsRepository doctorExaminationEventsRepository)
        {
            _doctorExaminationEventsRepository = doctorExaminationEventsRepository;
        }

        public void Execute(int id, String report)
        {
            DoctorExamination doctorExamination = _doctorExaminationEventsRepository.findById(id);

            doctorExamination.ChooseReport(report);

            _doctorExaminationEventsRepository.AddExaminationTimeEvent(doctorExamination);
        }
    }
}
