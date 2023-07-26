using System;
using System.ComponentModel.DataAnnotations;

namespace IntegrationAPI.DTO
{
    public class ReportSettingsDTO : BaseModelDTO
    {
        [Required]
        public DateTime StartDeliveryDate { get; set; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int DeliveryYears { get; set; }
        [Required]
        [Range(0, 11)]
        public int DeliveryMonths { get; set; }
        [Required]
        [Range(0, 31)]
        public int DeliveryDays { get; set; }
        [Required]
        [Range(0, Double.PositiveInfinity)]
        public int CalculationYears { get; set; }
        [Required]
        [Range(0, 11)]
        public int CalculationMonths { get; set; }
        [Required]
        [Range(0, 31)]
        public int CalculationDays { get; set; }
    }
}
