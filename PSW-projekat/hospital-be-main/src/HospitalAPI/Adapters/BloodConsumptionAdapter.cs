using HospitalAPI.DTO;
using HospitalLibrary.Core.Model;
using System;

namespace HospitalAPI.Adapters
{
    public class BloodConsumptionAdapter
    {
        public static DoctorBloodConsumption FromDTO(BloodConsumptionDTO entity)
        {
            return new DoctorBloodConsumption()
            {
                Id = entity.Id,
                Blood = entity.Blood,
                Date = DateTime.Now,
                Purpose = entity.Purpose,
                Deleted = false,
                Doctor = null
    };
        }

        public static BloodConsumptionDTO ToDTO(DoctorBloodConsumption entity)
        {
            return new BloodConsumptionDTO()
            {
                Id = entity.Id,
                Blood = entity.Blood,
                DoctorId = entity.Doctor.Id,
                Purpose = entity.Purpose
            };
        }
    }
}
