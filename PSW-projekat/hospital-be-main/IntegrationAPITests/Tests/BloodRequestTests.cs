using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.DTO;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.BloodRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationAPITests.Tests
{
    public class BloodRequestTests : BaseIntegrationTest
    {
        public BloodRequestTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static BloodRequestController SetupSettingsController(IServiceScope scope)
        {
            return new BloodRequestController(scope.ServiceProvider.GetRequiredService<IBloodRequestService>());
        }

        [Fact]
        public void Create_request_for_blood()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            BloodRequestDTO testCase = new BloodRequestDTO()
            {
                Id = 1,
                RequiredForDate = DateTime.Now,
                BloodQuantity = 1,
                Reason = "",
                DoctorId = 1,
                RequestState =0,
                BloodType = 0,
            };

            var result = ((CreatedAtActionResult)controller.Create(testCase))?.Value as BloodRequest;
            Assert.NotNull(result);
        }
    }
}
