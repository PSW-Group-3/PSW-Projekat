using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.CRUD;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Repository.BloodRequests
{
    public interface IBloodRequestRepository : ICRUDRepository<BloodRequest>
    {
        IEnumerable<BloodRequest> GetAllByType(BloodType bloodType);
    }
}