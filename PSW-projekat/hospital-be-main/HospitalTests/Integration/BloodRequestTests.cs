using HospitalAPI;
using HospitalAPI.Controllers.PrivateApp;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.IntegrationConnection;
using HospitalLibrary.Core.Model.Enums;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.Integration
{
    public class BloodRequestTests : BaseIntegrationTest
    {
        public BloodRequestTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static BloodRequestController SetupSettingsController(IServiceScope scope)
        {
            return new BloodRequestController(scope.ServiceProvider.GetRequiredService<IIntegrationConnection>());
        }


        [Fact]
        public void Check_if_can_get_blood_requests()
        {

            var stubRepo = new Mock<IIntegrationConnection>();
            var requests = new List<BloodRequestDTO>();
            BloodRequestDTO b = new BloodRequestDTO()
            {
                BloodQuantity = 1,
                BloodType = BloodType.APlus,
                DoctorId = 4,
                Reason = "sadasddas",
                RequestState = RequestState.Pending,
                RequiredForDate = System.DateTime.Now.AddDays(1),
                Comment = "",
                BloodBankId = 1,
            };

            requests.Add(b);

            stubRepo.Setup(m => m.GetBloodRequests()).Returns(requests);

            BloodRequestController controller = new BloodRequestController(stubRepo.Object);
            var result = ((OkObjectResult)controller.GetAllRequest())?.Value as List<BloodRequestDTO>;
            result.ShouldBe(requests);
            JsonSerializer.Serialize(result).ShouldBe(JsonSerializer.Serialize(requests));

        }
    }
}
