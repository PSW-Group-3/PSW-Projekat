using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Protos;
using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO
{
    public class EmergencyBloodRequestDTO
    {
        [Required]
        public int BloodQuantity { get; set; }

        [Required]
        public BloodTypeProto BloodType { get; set; }

        [Required]
        public int BloodBankID { get; set; }
    }
}
