using IntegrationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.EmergencyBloodRequests
{
    public interface IEmergencyBloodRequestService
    {
        void RequestEmergencyBlood(EmergencyBloodRequestGRPC request);
        IEnumerable<EmergencyBloodRequest> GetAll();
    }
}
