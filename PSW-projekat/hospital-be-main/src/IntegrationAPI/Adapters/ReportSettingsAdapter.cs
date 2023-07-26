using IntegrationAPI.DTO;
using IntegrationLibrary.Core.Model;

namespace IntegrationAPI.Adapters
{
    public class ReportSettingsAdapter
    {
        public static ReportSettings FromDTO(ReportSettingsDTO entity)
        {
            return new ReportSettings()
            {
                CalculationDays = entity.CalculationDays,
                CalculationMonths = entity.CalculationMonths,
                CalculationYears = entity.CalculationYears,
                DeliveryDays = entity.DeliveryDays,
                DeliveryMonths = entity.DeliveryMonths,
                DeliveryYears = entity.DeliveryYears,
                Id = entity.Id,
                StartDeliveryDate = entity.StartDeliveryDate
            };
        }

        public static ReportSettingsDTO ToDTO(ReportSettings entity)
        {
            return new ReportSettingsDTO()
            {
                CalculationDays = entity.CalculationDays,
                CalculationMonths = entity.CalculationMonths,
                CalculationYears = entity.CalculationYears,
                DeliveryDays = entity.DeliveryDays,
                DeliveryMonths = entity.DeliveryMonths,
                DeliveryYears = entity.DeliveryYears,
                Id = entity.Id,
                StartDeliveryDate = entity.StartDeliveryDate
            };
        }
    }
}
