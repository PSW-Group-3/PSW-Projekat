using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.CRUD;

namespace IntegrationLibrary.Core.Repository.BloodBanks
{
    public interface IBloodBankRepository : ICRUDRepository<BloodBank>
    {
        bool CheckIfAPIKeyExists(string apiKey);

        bool CheckIfAPIKeyIsUpdatable(BloodBank bank);

        bool CheckIfEmailExists(Email email);

        bool CheckIfEmailIsUpdatable(BloodBank bank);

        bool CheckIfPasswordResetKeyExists(string passwordResetKey);

        bool CheckIfPasswordResetKeyIsUpdatable(BloodBank bank);

        BloodBank GetBloodBankFromPasswordResetKey(string passwordResetKey);
        BloodBank getByEmail(string email);
    }
}
