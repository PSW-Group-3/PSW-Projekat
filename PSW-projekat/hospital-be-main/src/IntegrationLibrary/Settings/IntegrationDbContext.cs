using IntegrationLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IntegrationLibrary.Settings
{
    public class IntegrationDbContext : DbContext
    {
        public DbSet<BloodBank> BloodBanks { get; set; }

        public DbSet<ReportSettings> ReportSettings { get; set; }


        public DbSet<News> Newses { get; set; }

        public DbSet<BloodRequest> BloodRequests { get; set; }


        public IntegrationDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BloodBank>().HasData(
                new BloodBank() { Id = 1 , Name = "asdsadsda", Email = "asdasd@gmail.com", Password = "asdsadsdadas" , ApiKey = "sadfasdads" , ServerAddress = "https://www.messenger.com/t/100001603572170", AccountStatus = AccountStatus.ACTIVE } 
            );
            modelBuilder.Entity<ReportSettings>().HasData(
                new ReportSettings()
                {
                    CalculationDays = 0,
                    CalculationMonths = 1,
                    CalculationYears = 0,
                    DeliveryDays = 0,
                    DeliveryMonths = 1,
                    DeliveryYears = 0,
                    Id = 1,
                    StartDeliveryDate = Microsoft.VisualBasic.DateAndTime.Today
                }
                );
            modelBuilder.Entity<News>().HasData(
                new News()
                {
                    Id = 2,
                    Status = NewsStatus.PENDING,
                    Text = "Come and give me blood!",
                    Title = "First blood of the year!",
                    BloodBankId = 1,
                    DateTime = Microsoft.VisualBasic.DateAndTime.Today
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
