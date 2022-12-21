using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.ScheduledOrders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Xunit;
using IntegrationLibrary.Core.Service.BloodBanks;

namespace IntegrationAPITests.Tests
{
    public class SheduledOrderTests : BaseIntegrationTest
    {
        public SheduledOrderTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { 
        }
        private static ScheduledOrderController SetupSettingsController(IServiceProvider scope)
        {
            return new ScheduledOrderController(scope.GetRequiredService<IScheduledOrderService>(), scope.GetRequiredService<IBloodBankService>());
        }
        [Fact]
        public void Create_scheduled_order()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController((IServiceProvider)scope);

            ScheduledOrder testCase = new ScheduledOrder()
            {
                APlus = 10,
                BPlus = 20,
                BankEmail = "test@gmail.com",
                DayOfMonth = 1,
                HospitalEmail = "our.hospital@gmail.com",
                Id = 0
            };
            var result = ((OkObjectResult)controller.Create(testCase))?.Value as ScheduledOrder;
            result.Id = 0;
            result.ShouldBe(testCase);
            JsonSerializer.Serialize(result).ShouldBe(JsonSerializer.Serialize(testCase));
        }
    }
}
