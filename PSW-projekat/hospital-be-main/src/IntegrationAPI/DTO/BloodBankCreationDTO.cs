using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO
{
    public class BloodBankCreationDTO
    {
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Url]
        [Required]
        public string ServerAddress { get; set; }

        [Url]
        public string GRPCServerAddress { get; set; }
    }
}
