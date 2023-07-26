using IntegrationAPI;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.BloodBankConnection;
using IntegrationLibrary.Core.HospitalConnection;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.BloodRequests;
using IntegrationLibrary.Core.Service;
using IntegrationLibrary.Core.Service.BloodBanks;
using IntegrationLibrary.Core.Service.BloodRequests;
using IntegrationLibrary.Core.Service.Reports;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;

namespace IntegrationAPITests.Tests
{
    public class BloodBankReportPDFTests : BaseIntegrationTest
    {
        public BloodBankReportPDFTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }
 
        private static ReportSendingService SetupSettingsService(IServiceScope scope)
        {
            var mockConnection = new Mock<IBloodBankConnection>();
            return new ReportSendingService(mockConnection.Object,
                                scope.ServiceProvider.GetRequiredService<IReportSettingsService>(),
                                scope.ServiceProvider.GetRequiredService<IBloodBankService>(), scope.ServiceProvider.GetRequiredService<IBloodRequestService>(),
                                scope.ServiceProvider.GetRequiredService<IEmailService>());
        }

        [Fact]
        public void Create_PDF_report()
        {
            using var scope = Factory.Services.CreateScope();
            var generator = SetupSettingsService(scope);

            var result = generator.GeneratePDFs();
            String path = Directory.GetParent(Directory.GetCurrentDirectory()).FullName + @"\net5.0\BloodReportForprva_" + DateTime.Now.ToString("ddMMyyyy") + ".pdf";
            Assert.True(File.Exists(path));
        }


        [Fact]
        public void Find_fulfilled_requests_for_bank()
        {
            using var scope = Factory.Services.CreateScope();
            BloodRequestService service = new BloodRequestService(CreateStubRepository(), scope.ServiceProvider.GetRequiredService<IBloodBankService>(), 
                scope.ServiceProvider.GetRequiredService<IHospitalConnection>());

            List<BloodRequest> requests = (List<BloodRequest>)service.GetFulfilledRequests(7);
            Assert.Equal(CreateRequestList().Count(), requests.Count());
        }

        private static IBloodRequestRepository CreateStubRepository() {
            var stubRepository = new Mock<IBloodRequestRepository>();
            var requests = new List<BloodRequest>();

            BloodRequest req1 = new BloodRequest(new DateTime(2022, 12, 1), new Quantity(2), "for operation", 1, RequestState.Fulfilled, BloodType.BP, 7, null);
            BloodRequest req2 = new BloodRequest(new DateTime(2022, 12, 3), new Quantity(3), "for operation", 1, RequestState.Fulfilled, BloodType.BN, 7, null);
            BloodRequest req3 = new BloodRequest(new DateTime(2022, 12, 12), new Quantity(1), "for operation", 1, RequestState.Fulfilled, BloodType.AP, 8, null);
            BloodRequest req4 = new BloodRequest(new DateTime(2022, 12, 22), new Quantity(4), "for operation", 1, RequestState.Pending, BloodType.ABN, 8, null);

            requests.Add(req1);
            requests.Add(req2);
            requests.Add(req3);
            requests.Add(req4);

            stubRepository.Setup(x => x.GetAll()).Returns(requests);
            return stubRepository.Object;
        }
        private static List<BloodRequest> CreateRequestList(){
            var requestList = new List<BloodRequest>();
            BloodRequest req1 = new BloodRequest(new DateTime(2022, 12, 1), new Quantity(2), "for operation", 1, RequestState.Fulfilled, BloodType.BP, 7, null);
            BloodRequest req2 = new BloodRequest(new DateTime(2022, 12, 3), new Quantity(3), "for operation", 1, RequestState.Fulfilled, BloodType.BN, 7, null);
            requestList.Add(req1);
            requestList.Add(req2);
            return requestList;
        }
        
    }
}
