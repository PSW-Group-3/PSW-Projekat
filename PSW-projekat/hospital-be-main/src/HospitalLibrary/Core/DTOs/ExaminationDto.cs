using System;
using System.Collections.Generic;
using HospitalLibrary.Core.Model;

namespace HospitalLibrary.Core.DTOs
{
    public class ExaminationDto
    {
        public int Id { get; set; }
        public virtual AppointmentDto AppointmentDto { get; set; }
        public virtual ICollection<Prescription> Prescriptions { get; set; }
        public virtual ICollection<Symptom> Symptoms { get; set; }
        public String Report { get; set; }

        public ExaminationDto() { }

        public ExaminationDto(int id, AppointmentDto appointmentDto, ICollection<Prescription> prescriptions, ICollection<Symptom> symptoms, string report)
        {
            Id = id;
            AppointmentDto = appointmentDto;
            Prescriptions = prescriptions;
            Symptoms = symptoms;
            Report = report;
        }
    }
}
