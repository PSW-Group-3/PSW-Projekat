using HospitalLibrary.Core.Model;

namespace IntegrationLibrary.Core.HospitalConnection
{
    public interface IHospitalConnection
    {
        bool StoreBlood(Blood blood);
    }
}
