using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }
        public DbSet<Allergy> Allergies { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

        public DbSet<PatientAllergies> PatientAllergies { get; set; }


        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", Floor = 1 },
                new Room() { Id = 2, Number = "204", Floor = 2 },
                new Room() { Id = 3, Number = "305B", Floor = 3 }
            );

            modelBuilder.Entity<Allergy>().HasData(
                new Allergy() { Id = 1, Name = "Polen", Deleted = false },
                new Allergy() { Id = 2, Name = "Prasina", Deleted = false },
                new Allergy() { Id = 3, Name = "Pas", Deleted = false },
                new Allergy() { Id = 4, Name = "Macka", Deleted = false },
                new Allergy() { Id = 5, Name = "Pcela", Deleted = false },
                new Allergy() { Id = 6, Name = "Ambrozija", Deleted = false },
                new Allergy() { Id = 7, Name = "Kikiriki", Deleted = false },
                new Allergy() { Id = 8, Name = "Gluten", Deleted = false },
                new Allergy() { Id = 9, Name = "Laktoza", Deleted = false },
                new Allergy() { Id = 10, Name = "Alergija10", Deleted = false },
                new Allergy() { Id = 11, Name = "Alergija11", Deleted = false },
                new Allergy() { Id = 12, Name = "Alergija12", Deleted = false },
                new Allergy() { Id = 13, Name = "Alergija13", Deleted = false }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
