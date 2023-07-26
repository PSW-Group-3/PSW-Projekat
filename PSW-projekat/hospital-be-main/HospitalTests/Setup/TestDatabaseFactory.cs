using HospitalAPI;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using HospitalLibrary.Identity;

namespace HospitalTests.Setup
{
    public class TestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                using var scope = BuildServiceProvider(services).CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<HospitalDbContext>();

                InitializeDatabase(db);
            });
        }

        private static ServiceProvider BuildServiceProvider(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<HospitalDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<HospitalDbContext>(opt => opt.UseSqlServer(CreateConnectionStringForTest()).UseLazyLoadingProxies());
            services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(CreateConnectionStringForTest()));
            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Server=.;Database=HospitalDb;TrustServerCertificate=False;Trusted_Connection=True";
        }

        private static void InitializeDatabase(HospitalDbContext context)
        {
            context.Database.EnsureCreated();
            /*
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"BloodBanks\";");
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"ReportSettings\";");
            context.BloodBanks.Add(new BloodBank {
                Name = "asdsadsda",
                Email = "asdasd@gmail.com",
                Password = "asdsadsdadas",
                ApiKey = "sadfasdads",
                ServerAddress = "https://www.messenger.com/t/100001603572170",
                AccountStatus = AccountStatus.ACTIVE
            });
            context.BloodBanks.Add(new BloodBank {
                Name = "aa",
                Email = "asdasd@gmail.com",
                Password = "asdsadsdadas",
                ApiKey = "sadfasdads",
                ServerAddress = "https://www.messenger.com/t/100001603572170",
                AccountStatus = AccountStatus.ACTIVE
            });
            */

            /*
            context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Allergies\";");
            context.Allergies.Add(new Allergy() { Id = 1, Name = "Polen", Deleted = false });
            context.Allergies.Add(new Allergy() { Id = 2, Name = "Prasina", Deleted = false });
            context.Allergies.Add(new Allergy() { Id = 3, Name = "Pas", Deleted = false });
            context.Allergies.Add(new Allergy() { Id = 4, Name = "Macka", Deleted = false });

            context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Doctors\";");
            context.Doctors.Add(new Doctor()
            { 
                Id = 1,
                Specialization = 0,
                Person = new Person()
                {
                    Address = new Address() { City="a",Deleted=false,Id=1,Number="1",PostCode="13",Street="ulica",Township="asdasd" },
                    Id=1,Deleted=false,
                    BirthDate=System.DateTime.Now,
                    Email="a@a",
                    Gender=0,
                    Name="dasd",
                    Role=HospitalLibrary.Core.Model.Enums.Role.doctor,
                    Surname="asd"
                },
                Deleted = false,
                Patients= null 
            });
            */
            context.SaveChanges();
        }
    }
}
