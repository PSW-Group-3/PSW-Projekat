using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.CRUD;
using System;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.BloodBanks
{
    public interface IBloodBankService : ICRUDService<BloodBank>
    {
        bool CheckIfPasswordResetKeyExists(string passwordResetKey);
        BloodBank GetBloodBankFromPasswordResetKey(string passwordResetKey);
        Boolean SendBloodRequest(int bloodBankID, String BloodType, int quantity);
        void CheckBloodRequest(int bloodBankID, String BloodType, int quantity);
        Boolean CheckIfExists(String username, String password);
        Task<int> GetBlood(BloodBank bank, BloodType bloodType, int quantity);
    }
}
