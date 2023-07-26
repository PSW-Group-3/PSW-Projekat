using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.DTO;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Service.Tenders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace IntegrationAPITests.Tests
{
    public class TenderTests : BaseIntegrationTest
    {
        public TenderTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }
        private static TenderController SetupSettingsController(IServiceScope scope)
        {
            return new TenderController(scope.ServiceProvider.GetRequiredService<ITenderService>());
        }

        [Fact]
        public void Create_tender()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            List <DemandDTO> demands = CreateDemandList();

            TenderDTO testCase = new TenderDTO()
            {
                DueDate = DateTime.Now,
                Demands = demands,
                State = TenderState.OPEN,
                Id = 0
            };

            var result = ((OkObjectResult)controller.Create(testCase))?.Value as TenderDTO;
            result.ShouldBe(testCase);
        }

        [Fact]
        public void Bid_on_tender()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);



            var result = (OkResult)controller.BidOnTender(1, new BidDTO()
            {
                DeliveryDate = DateTime.Now,
                Price = 3000,
                BloodBankId = 1
            });
        }


        [Fact]
        public void Close_tender()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            var result = (OkResult)controller.CloseTender(1, 1);
        }

        private List<DemandDTO> CreateDemandList()
        {
            List<DemandDTO> demands = new List<DemandDTO>();

            DemandDTO testCase = new DemandDTO()
            {
                BloodType = BloodType.ABN,
                BloodQuantity = 3
            };
            DemandDTO testCase1 = new DemandDTO()
            {
                BloodType = BloodType.AN,
                BloodQuantity = 7
            };
            demands.Add(testCase);
            demands.Add(testCase1);
            return demands;
        }
    }
}
