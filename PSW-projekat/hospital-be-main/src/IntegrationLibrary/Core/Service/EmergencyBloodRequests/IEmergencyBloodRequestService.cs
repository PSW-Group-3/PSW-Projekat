using IntegrationLibrary.Core.Model;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Service.EmergencyBloodRequests
{
    public interface IEmergencyBloodRequestService
    {
        void RequestEmergencyBlood(EmergencyBloodRequestGRPC request);
        IEnumerable<EmergencyBloodRequest> GetAll();
        EmergencyBloodReport GetEmergencyBloodReportDT0(EmergencyBloodReportParams reportParams);
    }
}
