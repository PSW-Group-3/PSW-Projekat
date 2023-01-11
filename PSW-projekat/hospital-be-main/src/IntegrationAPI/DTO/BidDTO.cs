using IntegrationLibrary.Core.Model.Tender;
using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO
{
    public class BidDTO
    {
        [Required]
        public DateTime DeliveryDate { get; set; }
        [Required]
        public long Price { get; set; }
        [Required]
        public int BloodBankId { get; set; }
        [Required]
        public BidStatus Status { get; set; }
    }
}
