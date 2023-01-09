using System;
using System.Text.RegularExpressions;
using HospitalLibrary.Core.Model.Enums;

namespace HospitalLibrary.Core.Model
{
    public class Treatment: BaseModel
    {
        public virtual Patient Patient { get; set; }
        public DateTime DateAdmission { get; set; }
        public DateTime DateDischarge { get; set; }
        public string ReasonForAdmission { get; set; }
        public string ReasonForDischarge { get; set; }
        public TreatmentState TreatmentState { get; set; }
        public virtual Therapy Therapy { get; set; }
        public virtual Room Room { get; set; }

        public Treatment() { }

        public Treatment(Patient patient, DateTime dateAdmission, DateTime dateDischarge, string reasonForAdmission, 
            string reasonForDischarged, TreatmentState treatmentState, Therapy therapy, Room room)
        {
            if (Validate(patient, dateAdmission, dateDischarge, reasonForAdmission, reasonForDischarged, treatmentState, 
                therapy, room))
            {
                Patient = patient;
                DateAdmission = dateAdmission;
                DateDischarge = dateDischarge;
                ReasonForAdmission = reasonForAdmission;
                TreatmentState = treatmentState;
                Therapy = therapy;
                Room = room;
            }
            else
                throw new Exception("Invalid values for treatment");
        }

        private bool Validate(Patient patient, DateTime dateAdmission, DateTime dateDischarge, string reasonForAdmission,
            string reasonForDischarged, TreatmentState treatmentState, Therapy therapy, Room room)
        {
            DateTime wStart = new DateTime(1, 1, 1, dateAdmission.Hour, dateAdmission.Minute, dateAdmission.Second);
            DateTime wEnd = new DateTime(1, 1, 1, dateDischarge.Hour, dateDischarge.Minute, dateDischarge.Second);

            if (wEnd <= wStart)
            {
                return false;
            }

            Regex regex = new Regex(@"([A-Z]|[Č,Ć,Ž,Š,Đ]){1}(([a-z]|[č,ć,ž,š,đ])+)");
            if (!regex.IsMatch(reasonForAdmission) || !regex.IsMatch(reasonForDischarged))
                return false;

            return true;
        }

        static public Treatment Create(Treatment treatment)
        {
            Treatment treatment1 = new Treatment() { };
            treatment1.Patient = treatment.Patient;
            treatment1.DateAdmission = treatment.DateAdmission; 
            treatment1.DateDischarge = treatment.DateDischarge; 
            treatment1.ReasonForAdmission = treatment.ReasonForAdmission;
            treatment1.ReasonForDischarge = treatment.ReasonForDischarge;
            treatment1.TreatmentState = TreatmentState.open;
            treatment1.Therapy = treatment.Therapy;
            treatment1.Room = treatment.Room;

            return treatment1;
        }


    }
}
