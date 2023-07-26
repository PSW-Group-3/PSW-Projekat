using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;
using System;
using System.Collections.Generic;

namespace HospitalAPI.DTO
{
    public class DoctorCouncilDTO:BaseModelDTO
    {
        public int DoctorId { get; set; }
        public string Topic { get; set; }
        public virtual ICollection<DoctorDto> Doctors { get; set; }
        public virtual ICollection<Specialization> Specializations { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Duration { get; set; }

    }
}
