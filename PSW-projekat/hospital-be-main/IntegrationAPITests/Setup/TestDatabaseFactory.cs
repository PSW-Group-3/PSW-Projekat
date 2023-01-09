using IntegrationAPI;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IntegrationAPITests.Setup
{
    public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                using var scope = BuildServiceProvider(services).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<IntegrationDbContext>();

                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<IntegrationDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<IntegrationDbContext>(opt => opt.UseSqlServer(CreateConnectionStringForTest()).UseLazyLoadingProxies());
            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Server=.;Database=IntegrationTestDb;TrustServerCertificate=False;Trusted_Connection=True";
        }

        private static void InitializeDatabase(IntegrationDbContext context)
        {
            context.Database.EnsureCreated();

            context.Database.ExecuteSqlRaw("TRUNCATE TABLE BloodBanks;");
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE ReportSettings;");
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE BloodRequests;");
            SetupTenderAndBids(context);

            context.BloodBanks.Add(new BloodBank("prva", "asdasd@gmail.com", "asdsadsdadas", "https://www.messenger.com/t/100001603572170", "sadfasdads", "asddsadasdsa", null, AccountStatus.ACTIVE));
            context.BloodBanks.Add(new BloodBank("aa", "asdasd@gmail.com", "asdsadsdadas", "https://www.messenger.com/t/100001603572170", "sadfasdads", "asddsadasdsa", null, AccountStatus.ACTIVE));
            context.BloodBanks.Add(new BloodBank("bb", "asdasd@gmail.com", "asdsadsdadas", "https://www.messenger.com/t/100001603572170", "sadfasdads", "asddsadasdsa", null, AccountStatus.ACTIVE));
            context.BloodBanks.Add(new BloodBank("rr", "asdasd@gmail.com", "asdsadsdadas", "https://www.messenger.com/t/100001603572170", "sadfasdads", "asddsadasdsa", null, AccountStatus.ACTIVE));

            context.ReportSettings.Add(new ReportSettings
            {
                CalculationDays = 0,
                CalculationMonths = 1,
                CalculationYears = 0,
                DeliveryDays = 1,
                DeliveryMonths = 0,
                DeliveryYears = 0,
                StartDeliveryDate = System.DateTime.Now.AddDays(-1),
            });
            context.BloodRequests.Add(new BloodRequest
            {
                BloodBankId = 1,
                BloodQuantity = new Quantity(2),
                BloodType = BloodType.ON,
                Reason = "For operation",
                RequestState = RequestState.Accepted,
                RequiredForDate = new System.DateTime(2022, 11, 15),
                DoctorId = 1

            });
            context.BloodRequests.Add(new BloodRequest
            {
                BloodBankId = 1,
                BloodQuantity = new Quantity(3),
                BloodType = BloodType.OP,
                Reason = "For operation",
                RequestState = RequestState.Accepted,
                RequiredForDate = new System.DateTime(2022, 11, 15),
                DoctorId = 1

            });

            context.BloodRequests.Add(new BloodRequest
            {
                BloodQuantity = new Quantity(1),
                BloodType = BloodType.BP,
                DoctorId = 4,
                Reason = "sadasddas",
                RequestState = RequestState.Pending,
                RequiredForDate = System.DateTime.MaxValue,
                Comment = ""
            });

            context.BloodRequests.Add(new BloodRequest
            {
                BloodQuantity = new Quantity(5),
                BloodType = BloodType.BN,
                DoctorId = 2,
                Reason = "asdasddas",
                RequestState = RequestState.Pending,
                RequiredForDate = System.DateTime.MaxValue,
                Comment = ""
            });

            context.BloodRequests.Add(new BloodRequest
            {
                BloodQuantity = new Quantity(5),
                BloodType = BloodType.BN,
                DoctorId = 1,
                Reason = "asdasddas",
                RequestState = RequestState.Accepted,
                RequiredForDate = System.DateTime.MaxValue,
                Comment = ""
            });

            context.BloodRequests.Add(new BloodRequest
            {
                BloodQuantity = new Quantity(5),
                BloodType = BloodType.BN,
                DoctorId = 3,
                Reason = "asdasddas",
                RequestState = RequestState.Returned,
                RequiredForDate = System.DateTime.MaxValue,
                Comment = "asddaswreqwreqwr"
            });

            context.BloodRequests.Add(new BloodRequest
            {
                BloodQuantity = new Quantity(5),
                BloodType = BloodType.ON,
                DoctorId = 2,
                Reason = "asdasddas",
                RequestState = RequestState.Declined,
                RequiredForDate = System.DateTime.MaxValue,
                Comment = ""
            });
            context.BloodRequests.Add(new BloodRequest
            {
                BloodQuantity = new Quantity(2),
                BloodType = BloodType.ON,
                DoctorId = 2,
                Reason = "asdasddas",
                RequestState = RequestState.Fulfilled,
                RequiredForDate = System.DateTime.MaxValue,
                Comment = "",
                BloodBankId = 1,
            });
            context.BloodRequests.Add(new BloodRequest
            {
                BloodQuantity = new Quantity(2),
                BloodType = BloodType.OP,
                DoctorId = 2,
                Reason = "asdasddas",
                RequestState = RequestState.Fulfilled,
                RequiredForDate = System.DateTime.MaxValue,
                Comment = "",
                BloodBankId = 1,
            });
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Newses\";");
            context.Newses.Add(new News
            {
                Status = NewsStatus.PENDING,
                Title = "Blood donation",
                Text = " Come and give me blood",
                DateTime = new DateTime(2022, 01, 01, 9, 15, 0),
                BloodBankId = 1,
            });

            context.SaveChanges();
        }

        private static void SetupTenderAndBids(IntegrationDbContext context)
        {
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Bids;");
            context.Database.ExecuteSqlRaw("ALTER TABLE Bids\n" +
                "DROP CONSTRAINT FK_Bids_Tenders_TenderId");
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE Tenders;");
            context.Database.ExecuteSqlRaw("ALTER TABLE Bids\n" +
                "ADD CONSTRAINT FK_Bids_Tenders_TenderId FOREIGN KEY (TenderId) REFERENCES Tenders(Id)");

            Tender tender1 = new Tender(DateTime.Now.AddDays(1), new List<Demand>()
            {
                new Demand(BloodType.AN, 5),
                new Demand(BloodType.AP, 9)
            });
            tender1.BidOnTender(new Bid(DateTime.MaxValue, 1000, 1));
            tender1.BidOnTender(new Bid(DateTime.Now, 1000, 2));
            context.Tenders.Add(tender1);
        }
    }
}
