using AutoMapper;
using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.DTO;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.HospitalConnection;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.BloodRequests;
using IntegrationLibrary.Core.Service.BloodBanks;
using IntegrationLibrary.Core.Service.BloodRequests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Text.Json;
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
                RequiredForDate = DateTime.Now,
                BloodQuantity = 1,
                Reason = "",
                DoctorId = 1,
                RequestState = 0,
                BloodType = 0,
                BloodBankId = 0,
                Id =0
            };

            var result = ((OkObjectResult)controller.Create(testCase))?.Value as BloodRequestDTO;
            result.Id = 0;
            result.ShouldBe(testCase);
            JsonSerializer.Serialize(result).ShouldBe(JsonSerializer.Serialize(testCase));
        }

        [Fact]
        public void Accept_request_for_blood()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            BloodRequestDTO testCase = new BloodRequestDTO()
            {
                BloodQuantity = 1,
                BloodType = BloodType.BP,
                DoctorId = 4,
                Reason = "sadasddas",
                RequestState = RequestState.Pending,
                RequiredForDate = System.DateTime.Now.AddDays(1),
                Comment = "",
                BloodBankId = 1,
                Id = 2
            };

            var result = ((OkResult)controller.AcceptRequest(testCase));
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Decline_request_for_blood()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            var result = ((OkResult)controller.DeclineRequest(2));
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Return_request_for_blood()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            var result = ((OkResult)controller.SendBackRequest(2, "reason"));
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Get_return_requests_for_doctor()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            var result = (OkObjectResult)controller.GetReturnedRequestsForDoctor(3);
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void Update_requests_from_doctor()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            BloodRequestDTO r = new BloodRequestDTO(5, new DateTime(2022,12,12), 3, "i need it", 3, RequestState.Returned, BloodType.ABP, "asddaswreqwreqwr", 0);

            var result = (OkResult)controller.UpdateFromDoctor(r);
            Assert.IsType<OkResult>(result);

        }

        [Fact]
        public void Get_requests_that_should_be_sent()
        {
            using var scope = Factory.Services.CreateScope();
            BloodRequestService service = new BloodRequestService(CreateStubRepository(), scope.ServiceProvider.GetRequiredService<IBloodBankService>(),
                scope.ServiceProvider.GetRequiredService<IHospitalConnection>());

            List<BloodRequest> requests = service.GetRequestsThatShouldBeSent();
            Assert.Equal(CreateRequestList().Count, requests.Count);
        }

        private static IBloodRequestRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IBloodRequestRepository>();
            var requests = new List<BloodRequest>();

            BloodRequest req1 = new BloodRequest(new DateTime(2022, 12, 1), new Quantity(2), "for operation", 1, RequestState.Accepted, BloodType.BP, 7, null);
            BloodRequest req2 = new BloodRequest(new DateTime(2022, 12, 31), new Quantity(3), "for operation", 1, RequestState.Accepted, BloodType.BN, 7, null);
            BloodRequest req3 = new BloodRequest(new DateTime(2022, 12, 12), new Quantity(1), "for operation", 1, RequestState.Accepted, BloodType.AP, 8, null);
            BloodRequest req4 = new BloodRequest(new DateTime(2022, 12, 22), new Quantity(4), "for operation", 1, RequestState.Pending, BloodType.ABN, 8, null);

            requests.Add(req1);
            requests.Add(req2);
            requests.Add(req3);
            requests.Add(req4);

            stubRepository.Setup(x => x.GetAll()).Returns(requests);
            return stubRepository.Object;
        }
        private static List<BloodRequest> CreateRequestList()
        {
            var requestList = new List<BloodRequest>();
            BloodRequest req1 = new BloodRequest(new DateTime(2022, 12, 1), new Quantity(2), "for operation", 1, RequestState.Accepted, BloodType.BP, 7, null);
            BloodRequest req2 = new BloodRequest(new DateTime(2022, 12, 12), new Quantity(1), "for operation", 1, RequestState.Accepted, BloodType.AP, 8, null);
            requestList.Add(req1);
            requestList.Add(req2);
            return requestList;
        }
    }
}
