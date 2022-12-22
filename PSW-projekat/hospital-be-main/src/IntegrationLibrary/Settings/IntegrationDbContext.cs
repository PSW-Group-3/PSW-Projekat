using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.Tender;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MimeKit;
using Newtonsoft.Json;
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
        public DbSet<ScheduledOrder> ScheduledOrders { get; set; }
        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Demand> Demands { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Offer> Offers { get; set; }


        public IntegrationDbContext([NotNull] DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BloodBank>()
                .Property(b => b.Email)
                .HasConversion((save) => JsonConvert.SerializeObject(save), read => JsonConvert.DeserializeObject<Email>(read));

            base.OnModelCreating(modelBuilder);
        }
    }
}
