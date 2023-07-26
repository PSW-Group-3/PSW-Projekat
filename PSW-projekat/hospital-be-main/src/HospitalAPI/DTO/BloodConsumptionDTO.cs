using HospitalLibrary.Core.Model;
using System;

namespace HospitalAPI.DTO
{
    public class BloodConsumptionDTO : BaseModelDTO
    {
        public Blood Blood { get; set; }
        public String Purpose { get; set; }
        public int DoctorId { get; set; }

    }
}
