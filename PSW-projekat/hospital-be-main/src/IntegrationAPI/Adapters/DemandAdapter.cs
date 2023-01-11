using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model.Tender;

namespace IntegrationAPI.Adapters
{
    public class DemandAdapter
    {
        public static Demand FromDTO(DemandDTO entity)
        {
            return new Demand(entity.BloodType, entity.BloodQuantity);
        }

        public static DemandDTO ToDTO(Demand entity)
        {
            return new DemandDTO()
            {
                BloodType = entity.BloodType,
                BloodQuantity = entity.Quantity
            };
        }
    }
}
