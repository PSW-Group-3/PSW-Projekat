using HospitalAPI;
using HospitalAPI.Controllers.PublicApp;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace HospitalTests.Integration
{
    public class BloodTest : BaseIntegrationTest
    {
        public BloodTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }
        private static BloodController SetupSettingsController(IServiceScope scope)
        {
            return new BloodController(scope.ServiceProvider.GetRequiredService<IBloodService>());
        }
        [Fact]
        public void test()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);
            List<BloodOrderDto> orders = new List<BloodOrderDto>();
            BloodOrderDto order = new BloodOrderDto
            {
                APlus = 1,
                BPlus = 2,
                BankEmail = "bloodymary@gmail.com",
                IsSent = false
            };
            orders.Append(order);
            var result = ((OkObjectResult)controller.TakeOrder(orders))?.Value as List<BloodOrderDto>;
            result.ShouldBe(orders);
            JsonSerializer.Serialize(result).ShouldBe(JsonSerializer.Serialize(orders));
        }
    }
}
