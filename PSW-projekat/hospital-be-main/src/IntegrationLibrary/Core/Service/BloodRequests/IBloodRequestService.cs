using IntegrationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.BloodRequests
{
    public interface IBloodRequestService
    {
        void Create(BloodRequest entity);
        BloodRequest GetById(int id);
    }
}
