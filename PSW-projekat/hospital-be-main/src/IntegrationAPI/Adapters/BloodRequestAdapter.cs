using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model;

namespace IntegrationAPI.Adapters
{
    public class BloodRequestAdapter
    {
        public static BloodRequest FromDTO(BloodRequestDTO entity)
        {
            return new BloodRequest()
            {
                RequiredForDate = entity.RequiredForDate,
                BloodQuantity = new Quantity(entity.BloodQuantity),
                Reason = entity.Reason,
                DoctorId = entity.DoctorId,
                RequestState = entity.RequestState,
                BloodType = entity.BloodType,
                Id = entity.Id,
                BloodBankId = entity.BloodBankId
            };
        }

        public static BloodRequestDTO ToDTO(BloodRequest entity)
        {
            return new BloodRequestDTO()
            {
                RequiredForDate = entity.RequiredForDate,
                BloodQuantity = entity.BloodQuantity.Value,
                Comment = entity.Comment,
                Reason = entity.Reason,
                DoctorId = entity.DoctorId,
                RequestState = entity.RequestState,
                BloodType = entity.BloodType,
                Id = entity.Id,
                BloodBankId = entity.BloodBankId
            };
        }
    }
}
