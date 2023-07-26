using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.Model
{
    public class Doctor : BaseModel
    {
        public Specialization Specialization { get; set; }
        public virtual Person Person { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<DoctorsCouncil> Councils { get; set; }
        public virtual ICollection<DoctorSchedule> DoctorSchedules { get; set; }

        public Doctor()
        {

        }

        public Doctor(
            Specialization specialization,
            Person person,
            ICollection<Patient> patients,
            ICollection<DoctorsCouncil> councils,
            ICollection<DoctorSchedule> doctorSchedules
            )
        {
            if (Validate(person))
            {
                this.Specialization = specialization;
                this.Person = person;
                this.Patients = patients;
                this.Councils = councils;
                this.DoctorSchedules = doctorSchedules;
            }
            else
            {
                throw new Exception("Invalid values for doctor");
            }
                
        }

        private bool Validate(
            Person person
            )
        {
            if (person != null)
            {
                return true;
            }  
            return false;
        }

    }
}
