using IntegrationLibrary.Protos;

namespace IntegrationLibrary.Core.Model
{
    public class EmergencyBloodRequestGRPC
    {
        public int BloodQuantity { get; set; }

        public BloodTypeProto BloodType { get; set; }

        public int BloodBankID { get; set; }
    }
}
