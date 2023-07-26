using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.DTO;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.Service.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using Xunit;

namespace IntegrationAPITests.Tests
{
    public class ReportSettingsTest : BaseIntegrationTest
    {
        public ReportSettingsTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static ReportSettingsController SetupSettingsController(IServiceScope scope)
        {
            return new ReportSettingsController(scope.ServiceProvider.GetRequiredService<IReportSettingsService>());
        }

        [Fact]
        public void Check_if_can_update_settings()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            ReportSettingsDTO testCase = new ReportSettingsDTO()
            {
                Id = 1,
                CalculationDays = 0,
                CalculationMonths = 0,
                CalculationYears = 1,
                DeliveryDays = 0,
                DeliveryMonths = 0,
                DeliveryYears = 1,
                StartDeliveryDate = DateTime.MinValue
            };

            var result = ((OkObjectResult)controller.Update(testCase))?.Value as ReportSettingsDTO;
            result.ShouldBe(testCase);
        }
    }
}
