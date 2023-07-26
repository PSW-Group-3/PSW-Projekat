using System;

namespace HospitalLibrary.Core.AggregatDoctor.Events
{
    public class DoctorWriteReport : DomainEvent
    {
        public string Report { get; set; }

        public DoctorWriteReport()
        {
            phase = ExaminationStage.symptomsChoosen;
            selectionTime = DateTime.Now;
        }

        public DoctorWriteReport(String report)
        {
            phase = ExaminationStage.symptomsChoosen;
            Report = report;
            selectionTime = DateTime.Now;
        }
    }
}
