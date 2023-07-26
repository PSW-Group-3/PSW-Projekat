using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.CRUD;
using System;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Service.BloodRequests
{
    public interface IBloodRequestService : ICRUDService<BloodRequest>
    {
        void AcceptRequest(BloodRequest request);
        void DeclineRequest(int id);
        void SendBackRequest(int id, string reason);
        List<BloodRequest> GetReturnedRequestsForDoctor(int id);
        void UpdateFromDoctor(BloodRequest request);
        IEnumerable<BloodRequest> GetFulfilledRequests(int id);
        Boolean RequestShouldBeSent();
        List<BloodRequest> GetAcceptedRequests();
        List<BloodRequest> GetRequestsThatShouldBeSent();
        public void GetBloodFromBloodBank(BloodRequest request);
        IEnumerable<BloodRequest> GetAllByType(BloodType bloodType);
    }
}
