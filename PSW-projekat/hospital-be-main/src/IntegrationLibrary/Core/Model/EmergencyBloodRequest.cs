using System;

namespace IntegrationLibrary.Core.Model
{
    public class EmergencyBloodRequest : EntityClass
    {
        public int BloodQuantity { get; set; }
        public BloodType BloodType { get; set; }
        public int BloodBankId { get; set; }
        public DateTime Date { get; set; }
    }
}
