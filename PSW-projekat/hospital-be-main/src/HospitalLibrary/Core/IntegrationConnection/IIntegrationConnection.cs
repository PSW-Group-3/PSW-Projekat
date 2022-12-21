using HospitalLibrary.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.IntegrationConnection
{
    public interface IIntegrationConnection
    {
        public bool CheckIfExists(LoginUserDto _user);
        public List<BloodRequestDTO> GetBloodRequests();
    }
}
