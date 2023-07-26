using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO
{
    public class BloodBankDTO : BaseModelDTO
    {
        public string Name { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [Url]
        public string ServerAddress { get; set; }

        public string ApiKey { get; set; }
    }
}
