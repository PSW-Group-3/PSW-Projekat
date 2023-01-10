using HospitalAPI;
using HospitalLibrary.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Identity;
using HospitalLibrary.Core.Model.Enums;
using System;
using HospitalLibrary.Core.Model.Aggregate;

namespace HospitalTests.Setup
{
    public class StatisticsTestDatabaseFactory<TStartup> : WebApplicationFactory<Startup>
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
            //services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(CreateConnectionStringForTest()));
            return services.BuildServiceProvider();
        }

        private static string CreateConnectionStringForTest()
        {
            return "Server=.;Database=HospitalStatisticsTestDb;TrustServerCertificate=False;Trusted_Connection=True";
        }

        private static void InitializeDatabase(HospitalDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Database.ExecuteSqlRaw("TRUNCATE TABLE \"Allergies\";");
            var allergy1 = new Allergy() { Name = "Polen", Deleted = false };
            context.Allergies.Add(allergy1);
            context.Allergies.Add(new Allergy() { Name = "Prasina", Deleted = false });
            context.Allergies.Add(new Allergy() {  Name = "Pas", Deleted = false });
            context.Allergies.Add(new Allergy() {  Name = "Macka", Deleted = false });

            var address = new Address(){City = "a", Number = "1", PostCode = "13", Street = "ulica", Township = "asdasd"};

            var person1 = new Person(){Address = address, Deleted = false, BirthDate = System.DateTime.Now, Email = Email.Create("milan@gmail.com"), Gender = 0, Name = "Milan", Role = HospitalLibrary.Core.Model.Enums.Role.doctor, Surname = "Milovanovic"};
            var person2 = new Person(){Address = address, Deleted = false, BirthDate = System.DateTime.Now, Email = Email.Create("milos@gmail.com"), Gender = 0, Name = "Milos", Role = HospitalLibrary.Core.Model.Enums.Role.doctor, Surname = "Milosevic" };
            var person3 = new Person(){Address = address, Deleted = false, BirthDate = new System.DateTime(2000,12,12), Email = Email.Create("jovana@gmail.com"), Gender = Gender.female, Name = "Jovana", Role = HospitalLibrary.Core.Model.Enums.Role.patient, Surname = "Jovanovic" };
            var person4 = new Person(){Address = address, Deleted = false, BirthDate = new System.DateTime(1990,12,12), Email = Email.Create("nikola@gmail.com"), Gender = 0, Name = "Nikola", Role = HospitalLibrary.Core.Model.Enums.Role.patient, Surname = "Nikolic" };
            
            var doctor1 = new Doctor() { Specialization = 0, Person = person1, Deleted = false, Patients = null };
            var doctor2 = new Doctor() { Specialization = 0, Person = person2, Deleted = false, Patients = null };

            var patient1 = new Patient() { BloodType = BloodType.APlus, Person = person3, Deleted = false, Doctor = doctor1 };
            var patient2 = new Patient() { BloodType = BloodType.APlus, Person = person4, Deleted = false, Doctor = doctor1 };

            var patientAllergy = new PatientAllergies() { AllergyId = 1, PatientId = 1 };

            context.Persons.Add(person1);
            context.Persons.Add(person2);
            context.Persons.Add(person3);
            context.Persons.Add(person4);

            context.Doctors.Add(doctor1);
            context.Doctors.Add(doctor2);

            context.Patients.Add(patient1);
            context.Patients.Add(patient2);
            context.PatientAllergies.Add(patientAllergy);

            context.ScheduleAppointmentByPatients.Add(new ScheduleAppointmentByPatient() { Id=0, Deleted=false, Stage=0, startTime=DateTime.Now });

            context.Appointments.Add(new Appointment() { Doctor = doctor1, Patient = patient1, Deleted = false, CancelationDate = new System.DateTime(2022, 10, 8), DateTime = new System.DateTime(2022, 10, 10) });
            context.Appointments.Add(new Appointment() { Doctor = doctor1, Patient = patient2, Deleted = false, CancelationDate = null, DateTime = new System.DateTime(2023, 2, 2) });

            context.PatientAllergies.Add(patientAllergy);

            context.SaveChanges();
        }
    }
}
