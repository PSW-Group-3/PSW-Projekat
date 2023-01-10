using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<WorkingDay> WorkingDays { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<PatientAllergies> PatientAllergies { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Blood> Bloods { get; set; }
        public DbSet<HistoryTreatment> HistoryTreatments { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Therapy> Therapys { get; set; }
        public DbSet<DoctorBloodConsumption> BloodConsumptions { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Prescription> Prescriptions { get; set; }
        public DbSet<Examination> Examinations { get; set; }

        public DbSet<DoctorsCouncil> DoctorsCouncils { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", RoomType = RoomType.rehabilitationRoom, Floor = 1, Deleted = false },
                new Room() { Id = 2, Number = "204", RoomType = RoomType.rehabilitationRoom, Floor = 2, Deleted = false },
                new Room() { Id = 3, Number = "305B", RoomType = RoomType.rehabilitationRoom, Floor = 3, Deleted = false },
                new Room() { Id = 4, Number = "STORAGE", RoomType = RoomType.storage, Floor = 3, Deleted = false }

            );
            */

            modelBuilder.Entity<Doctor>().OwnsMany(
                d => d.DoctorSchedules, ds =>
                {
                    ds.Property<Day>("Day");
                    ds.OwnsOne(ds => ds.Shift, tr =>
                    {
                        tr.OwnsOne(tr => tr.StartTime, st => {
                            st.Property(p => p.Hour);
                            st.Property(p => p.Minute);
                        });
                        tr.OwnsOne(tr => tr.EndTime, et => {
                            et.Property(p => p.Hour);
                            et.Property(p => p.Minute);
                        });
                    });
                    ds.Navigation(tr => tr.Shift);
                });
            modelBuilder.Entity<Doctor>(d => d.Navigation(d => d.DoctorSchedules));

            modelBuilder.Entity<Person>().OwnsOne(e => e.Email);

            modelBuilder.Entity<Person>().OwnsOne(e => e.Address);

            modelBuilder.Entity<Person>().OwnsOne(e => e.Jmbg);

            base.OnModelCreating(modelBuilder);
        }
    }
}
