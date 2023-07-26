using HospitalLibrary.Core.DTOs;
using System;
using System.Collections.Generic;

namespace HospitalLibrary.Core.IntegrationConnection
{
    public interface IIntegrationConnection
    {
        public bool CheckIfExists(LoginUserDto _user);
        public List<BloodRequestDTO> GetBloodRequests();
        public List<BloodRequestDTO> GetBloodRequestsByBlood(String bloodType);
    }
}
