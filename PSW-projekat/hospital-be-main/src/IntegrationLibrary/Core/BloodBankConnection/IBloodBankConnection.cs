using System;
using System.Threading.Tasks;
using IntegrationLibrary.Core.Model;

namespace IntegrationLibrary.Core.BloodBankConnection
{
    public interface IBloodBankConnection
    {
        Boolean SendBloodRequest(BloodBank bank, String bloodType, int quantity);
        Task<bool> SendBloodReports(BloodBank bank, byte[] pdf);
        Task<int> GetBlood(BloodBank bank, String bloodType, int quantity);
    }
}