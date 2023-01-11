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
using HospitalLibrary.Core.Model.Aggregate.Events;

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

            /*var patient3 = new Patient() { BloodType = BloodType.APlus, Person = person1, Deleted = false, Doctor = doctor1 };
            var patient4 = new Patient() { BloodType = BloodType.APlus, Person = person2, Deleted = false, Doctor = doctor1 };*/

            var aggregate1 = new ScheduleAppointmentByPatient() { Stage = SchedulingStage.appointmentScheduled, Patient = patient1, startTime = new DateTime(2023, 1, 9, 10, 25, 23), endTime = new DateTime(2023, 1, 9, 10, 26, 25), Changes = null };
            var aggregate2 = new ScheduleAppointmentByPatient() { Stage = SchedulingStage.appointmentScheduled, Patient = patient1, startTime = new DateTime(2023, 1, 10, 11, 36, 23), endTime = new DateTime(2023, 1, 10, 11, 37, 21), Changes = null };
            var aggregate3 = new ScheduleAppointmentByPatient() { Stage = SchedulingStage.dateChoosen,          Patient = patient1, startTime = new DateTime(2023, 1, 11, 10, 25, 23), endTime = new DateTime(2023, 1, 9, 11, 25, 27), Changes = null };
            var aggregate4 = new ScheduleAppointmentByPatient() { Stage = SchedulingStage.appointmentScheduled, Patient = patient1, startTime = new DateTime(2023, 1, 11, 10, 27, 20), endTime = new DateTime(2023, 1, 11, 10, 28, 25), Changes = null };

            var aggregate5 = new ScheduleAppointmentByPatient() { Stage = SchedulingStage.appointmentScheduled, Patient = patient2, startTime = new DateTime(2023, 1, 12, 10, 25, 23), endTime = new DateTime(2023, 1, 12, 10, 26, 25), Changes = null };
            var aggregate6 = new ScheduleAppointmentByPatient() { Stage = SchedulingStage.appointmentScheduled, Patient = patient2, startTime = new DateTime(2023, 1, 15, 11, 36, 23), endTime = new DateTime(2023, 1, 15, 11, 37, 10), Changes = null };
            
            /*var aggregate7 = new ScheduleAppointmentByPatient() { Id = 7, Stage = SchedulingStage.dateChoosen,          Patient = patient3, startTime = new DateTime(2023, 1, 13, 10, 25, 23), endTime = new DateTime(2023, 1, 13, 10, 25, 40), Changes = null };
            var aggregate8 = new ScheduleAppointmentByPatient() { Id = 8, Stage = SchedulingStage.appointmentScheduled, Patient = patient3, startTime = new DateTime(2023, 1, 13, 10, 27, 20), endTime = new DateTime(2023, 1, 13, 10, 28, 25), Changes = null };

            var aggregate9 =  new ScheduleAppointmentByPatient() { Id = 9,  Stage = SchedulingStage.appointmentScheduled, Patient = patient4, startTime = new DateTime(2023, 1, 10, 10, 25, 23), endTime = new DateTime(2023, 1, 10, 10, 26, 25), Changes = null };
            var aggregate10 = new ScheduleAppointmentByPatient() { Id = 10, Stage = SchedulingStage.appointmentScheduled, Patient = patient4, startTime = new DateTime(2023, 1, 15, 11, 36, 23), endTime = new DateTime(2023, 1, 15, 11, 37, 10), Changes = null };
            var aggregate11 = new ScheduleAppointmentByPatient() { Id = 11, Stage = SchedulingStage.doctorChoosen,        Patient = patient4, startTime = new DateTime(2023, 1, 17, 10, 25, 23), endTime = new DateTime(2023, 1, 17, 10, 25, 40), Changes = null };
            var aggregate12 = new ScheduleAppointmentByPatient() { Id = 12, Stage = SchedulingStage.doctorChoosen,        Patient = patient4, startTime = new DateTime(2023, 1, 18, 10, 27, 20), endTime = new DateTime(2023, 1, 18, 10, 28, 25), Changes = null };
            */
            var event1 = new PatientSelectedAppointmentDate()       { Aggregate = aggregate1, phase = SchedulingStage.dateChoosen,   selectionTime = new DateTime(2023, 1, 9, 10, 25, 24), selectedItem = "Mon Jan 15 2023",  Deleted = false };
            var event2 = new PatientSelectedDoctorSpecialization()  { Aggregate = aggregate1, phase = SchedulingStage.specChoosen,   selectionTime = new DateTime(2023, 1, 9, 10, 25, 29), selectedItem = "0",                Deleted = false };
            var event3 = new PatientSelectedDoctor()                { Aggregate = aggregate1, phase = SchedulingStage.doctorChoosen, selectionTime = new DateTime(2023, 1, 9, 10, 25, 38), selectedItem = "Marko Markovic",   Deleted = false };
            var event4 = new PatientSelectedAppointmentTime()       { Aggregate = aggregate1, phase = SchedulingStage.timeChoosen,   selectionTime = new DateTime(2023, 1, 9, 10, 25, 58), selectedItem = "11:00",            Deleted = false };

            var event5 =  new PatientSelectedAppointmentDate()      { Aggregate = aggregate2, phase = SchedulingStage.dateChoosen,   selectionTime = new DateTime(2023, 1, 10, 11, 36, 25), selectedItem = "Mon Jan 22 2023", Deleted = false };
            var event6 =  new PatientSelectedDoctorSpecialization() { Aggregate = aggregate2, phase = SchedulingStage.specChoosen,   selectionTime = new DateTime(2023, 1, 10, 11, 36, 35), selectedItem = "0",               Deleted = false };
            var event7 =  new PatientSelectedDoctor()               { Aggregate = aggregate2, phase = SchedulingStage.doctorChoosen, selectionTime = new DateTime(2023, 1, 10, 11, 36, 42), selectedItem = "Marko Markovic",  Deleted = false };
            var event8 =  new BackToDoctorSelection()               { Aggregate = aggregate2, phase = SchedulingStage.backToDoctor,  selectionTime = new DateTime(2023, 1, 10, 11, 36, 59), selectedItem = null,              Deleted = false };
            var event9 =  new PatientSelectedDoctor()               { Aggregate = aggregate2, phase = SchedulingStage.doctorChoosen, selectionTime = new DateTime(2023, 1, 10, 11, 37, 10), selectedItem = "Marko Markovic",  Deleted = false };
            var event10 = new PatientSelectedAppointmentTime()      { Aggregate = aggregate2, phase = SchedulingStage.timeChoosen,   selectionTime = new DateTime(2023, 1, 10, 11, 37, 20), selectedItem = "11:00",           Deleted = false };

            var event11 = new PatientSelectedAppointmentDate()      { Aggregate = aggregate3, phase = SchedulingStage.dateChoosen,   selectionTime = new DateTime(2023, 1, 11, 10, 25, 25), selectedItem = "Wed Jan 18 2023", Deleted = false };

            var event12 = new PatientSelectedAppointmentDate()      { Aggregate = aggregate4, phase = SchedulingStage.dateChoosen,   selectionTime = new DateTime(2023, 1, 11, 10, 27, 21), selectedItem = "Tue Jan 24 2023", Deleted = false };
            var event13 = new PatientSelectedDoctorSpecialization() { Aggregate = aggregate4, phase = SchedulingStage.specChoosen,   selectionTime = new DateTime(2023, 1, 11, 10, 27, 36), selectedItem = "0",               Deleted = false };
            var event14 = new PatientSelectedDoctor()               { Aggregate = aggregate4, phase = SchedulingStage.doctorChoosen, selectionTime = new DateTime(2023, 1, 11, 10, 27, 48), selectedItem = "Marko Markovic",  Deleted = false };
            var event15 = new PatientSelectedAppointmentTime()      { Aggregate = aggregate4, phase = SchedulingStage.timeChoosen,   selectionTime = new DateTime(2023, 1, 11, 10, 28, 23), selectedItem = "11:00",           Deleted = false };

            var event16 = new PatientSelectedAppointmentDate()      { Aggregate = aggregate5, phase = SchedulingStage.dateChoosen,   selectionTime = new DateTime(2023, 1, 12, 10, 25, 25), selectedItem = "Tue Jan 24 2023", Deleted = false };
            var event17 = new PatientSelectedDoctorSpecialization() { Aggregate = aggregate5, phase = SchedulingStage.specChoosen,   selectionTime = new DateTime(2023, 1, 12, 10, 25, 40), selectedItem = "0",               Deleted = false };
            var event18 = new PatientSelectedDoctor()               { Aggregate = aggregate5, phase = SchedulingStage.doctorChoosen, selectionTime = new DateTime(2023, 1, 12, 10, 25, 59), selectedItem = "Marko Markovic",  Deleted = false };
            var event19 = new PatientSelectedAppointmentTime()      { Aggregate = aggregate5, phase = SchedulingStage.timeChoosen,   selectionTime = new DateTime(2023, 1, 12, 10, 26, 23), selectedItem = "11:00",           Deleted = false };

            var event20 = new PatientSelectedAppointmentDate()      { Aggregate = aggregate6, phase = SchedulingStage.dateChoosen,   selectionTime = new DateTime(2023, 1, 15, 11, 36, 25), selectedItem = "Tue Jan 24 2023", Deleted = false };
            var event21 = new PatientSelectedDoctorSpecialization() { Aggregate = aggregate6, phase = SchedulingStage.specChoosen,   selectionTime = new DateTime(2023, 1, 15, 11, 36, 40), selectedItem = "0",               Deleted = false };
            var event22 = new PatientSelectedDoctor()               { Aggregate = aggregate6, phase = SchedulingStage.doctorChoosen, selectionTime = new DateTime(2023, 1, 15, 11, 36, 45), selectedItem = "Marko Markovic",  Deleted = false };
            var event23 = new BackToDoctorSelection()               { Aggregate = aggregate6, phase = SchedulingStage.backToDoctor,  selectionTime = new DateTime(2023, 1, 15, 11, 36, 48), selectedItem = null,              Deleted = false };
            var event24 = new PatientSelectedDoctor()               { Aggregate = aggregate6, phase = SchedulingStage.doctorChoosen, selectionTime = new DateTime(2023, 1, 15, 11, 36, 51), selectedItem = "Marko Markovic",  Deleted = false };
            var event25 = new PatientSelectedAppointmentTime()      { Aggregate = aggregate6, phase = SchedulingStage.timeChoosen,   selectionTime = new DateTime(2023, 1, 15, 11, 37, 9),  selectedItem = "11:00",           Deleted = false };

            context.ScheduleAppointmentByPatients.Add(aggregate1);
            context.ScheduleAppointmentByPatients.Add(aggregate2);
            context.ScheduleAppointmentByPatients.Add(aggregate3);
            context.ScheduleAppointmentByPatients.Add(aggregate4);
            context.ScheduleAppointmentByPatients.Add(aggregate5);
            context.ScheduleAppointmentByPatients.Add(aggregate6);

            context.AppointmentSchedulingEvents.Add(event1);
            context.AppointmentSchedulingEvents.Add(event2);
            context.AppointmentSchedulingEvents.Add(event3);
            context.AppointmentSchedulingEvents.Add(event4);
            context.AppointmentSchedulingEvents.Add(event5);
            context.AppointmentSchedulingEvents.Add(event6);
            context.AppointmentSchedulingEvents.Add(event7);
            context.AppointmentSchedulingEvents.Add(event8);
            context.AppointmentSchedulingEvents.Add(event9);
            context.AppointmentSchedulingEvents.Add(event10);
            context.AppointmentSchedulingEvents.Add(event11);
            context.AppointmentSchedulingEvents.Add(event12);
            context.AppointmentSchedulingEvents.Add(event13);
            context.AppointmentSchedulingEvents.Add(event14);
            context.AppointmentSchedulingEvents.Add(event15);
            context.AppointmentSchedulingEvents.Add(event16);
            context.AppointmentSchedulingEvents.Add(event17);
            context.AppointmentSchedulingEvents.Add(event18);
            context.AppointmentSchedulingEvents.Add(event19);
            context.AppointmentSchedulingEvents.Add(event20);
            context.AppointmentSchedulingEvents.Add(event21);
            context.AppointmentSchedulingEvents.Add(event22);
            context.AppointmentSchedulingEvents.Add(event23);
            context.AppointmentSchedulingEvents.Add(event24);
            context.AppointmentSchedulingEvents.Add(event25);


            context.SaveChanges();
        }
    }
}
