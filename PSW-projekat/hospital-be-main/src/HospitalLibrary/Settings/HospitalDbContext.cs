using HospitalLibrary.Core.AggregatDoctor;
using HospitalLibrary.Core.AggregatDoctor.Events;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Aggregate;
using HospitalLibrary.Core.Model.Aggregate.Events;
using HospitalLibrary.Core.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        public DbSet<Meal> Meals { get; set; }
        public DbSet<MealAnswer> MealAnswers { get; set; }
        public DbSet<MealQuestion> MealQuestions { get; set; }

        public DbSet<PatientHealthInformation> PatientHealthInformations { get; set; }

        public DbSet<Workout> Workouts { get; set; }
        public DbSet<GymWorkout> GymWorkouts { get; set; }

        public DbSet<DoctorExamination> DoctorExaminations { get; set; }
        public DbSet<Core.AggregatDoctor.DomainEvent> DoctorExaminationEvents { get; set; }

        public DbSet<ScheduleAppointmentByPatient> ScheduleAppointmentByPatients { get; set; }
        public DbSet<Core.Model.Aggregate.DomainEvent> AppointmentSchedulingEvents { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<DoctorSelectedSymptoms>();
            modelBuilder.Entity<DoctorSelectedPrescriptions>();
            modelBuilder.Entity<DoctorWriteReport>();
            modelBuilder.Entity<BackToSymptomsSelection>();
            modelBuilder.Entity<BackToReportWritten>();
            modelBuilder.Entity<BackToPrescriptionsSelection>();
            modelBuilder.Entity<PatientSelectedAppointmentTime>();
            modelBuilder.Entity<PatientSelectedDoctor>();
            modelBuilder.Entity<PatientSelectedDoctorSpecialization>();
            modelBuilder.Entity<BackToDoctorSelection>();
            modelBuilder.Entity<BackToSpecializationSelection>();
            modelBuilder.Entity<BackToAppointentTimeSelection>();
            modelBuilder.Entity<BackToAppointmentDateSelection>();
            modelBuilder.Entity<PatientSelectedAppointmentDate>();

            modelBuilder.Entity<Meal>().OwnsMany(meal => meal.Answers);
            modelBuilder.Entity<Meal>(meal => meal.Navigation(meal => meal.Answers));

            modelBuilder.Entity<GymWorkout>()
            .Property(e => e.Exercises)
            .HasColumnName("Exercises") // Specify the column name in the database
            .HasConversion(
                v => JsonConvert.SerializeObject(v), // Serialize List<Exercise> to JSON string
                v => JsonConvert.DeserializeObject<List<Exercise>>(v) // Deserialize JSON string to List<Exercise>
            )
            .HasColumnType("nvarchar(max)");

            base.OnModelCreating(modelBuilder);
        }
    }
}
