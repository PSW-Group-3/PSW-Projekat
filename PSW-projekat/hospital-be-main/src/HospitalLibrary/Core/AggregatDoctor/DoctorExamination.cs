using HospitalLibrary.Core.AggregatDoctor.Events;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.AggregatDoctor
{
    public class DoctorExamination : EventSourcedAggregate
    {
        public ExaminationStage Stage { get; set; } = ExaminationStage.beginning;
        public virtual Doctor Doctor { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }

        public DoctorExamination() { }

        public void ChooseSymptoms(List<Symptom> symptoms)
        {
            Causes(new DoctorSelectedSymptoms(symptoms));
        }

        public void ChoosePerscriptions(List<Prescription>  prescriptions)
        {
            Causes(new DoctorSelectedPrescriptions(prescriptions));
        }

        public void ChooseReport(String report)
        {
            Causes(new DoctorWriteReport(report));
        }

        

        public void BackToSymptomsChoosing()
        {
            Causes(new BackToSymptomsSelection());
        }

        public void BackToPrescriptionChoosing()
        {
            Causes(new BackToPrescriptionsSelection());
        }

        public void BackToReportChoosing()
        {
            Causes(new BackToReportWritten());
        }

        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        public override void Apply(DomainEvent @event)
        {
            When((dynamic)@event);
        }

        private void When(DoctorSelectedSymptoms doctorSelectedSymptoms)
        {
            doctorSelectedSymptoms.Aggregate = this;
            this.Stage = ExaminationStage.symptomsChoosen;
        }

        private void When(DoctorSelectedPrescriptions doctorSelectedPrescriptions)
        {
            doctorSelectedPrescriptions.Aggregate = this;
            this.Stage = ExaminationStage.prescriptionsChoosen;
        }

        private void When(DoctorWriteReport doctorWriteReport)
        {
            doctorWriteReport.Aggregate = this;
            this.Stage = ExaminationStage.reortWritten;
        }

        private void When(BackToSymptomsSelection backToSymptomsSelection)
        {
            backToSymptomsSelection.Aggregate = this;
            this.Stage = ExaminationStage.backToSymptoms;
        }

        private void When(BackToPrescriptionsSelection backToPrescriptionsSelection)
        {
            backToPrescriptionsSelection.Aggregate = this;
            this.Stage = ExaminationStage.backToPrescription;
        }

        private void When(BackToReportWritten backToReportWritten)
        {
            backToReportWritten.Aggregate = this;
            this.Stage = ExaminationStage.backToReort;
        }

        
        public IEnumerable<DomainEvent> GetChanges()
        {
            return Changes;
        }
    }
}
