using HospitalAPI;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using HospitalLibrary.Core.Repository;
using System.Collections.Generic;
using HospitalLibrary.Core.DTOs.CreatingAppointmentsDTOs;

namespace HospitalTests.Integration
{
    public class CustomAppointmentTests: BaseIntegrationTest
    {
        public CustomAppointmentTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        private static DoctorService SetupSettingsDoctorService(IServiceScope scope)
        {
            return new DoctorService(
                scope.ServiceProvider.GetRequiredService<IDoctorRepository>(), 
                scope.ServiceProvider.GetRequiredService<IPersonRepository>(),
                scope.ServiceProvider.GetRequiredService<AllergyRepository>()
                );
        }

        private static AppointmentService SetupSettingsPublicAppointmentService(IServiceScope scope)
        {
            return new AppointmentService(
                scope.ServiceProvider.GetRequiredService<IAppointmentRepository>(),
                scope.ServiceProvider.GetRequiredService<IWorkingDayRepository>(),
                scope.ServiceProvider.GetRequiredService<IDoctorRepository>(),
                scope.ServiceProvider.GetRequiredService<IPatientRepository>()
                );
        }

        [Fact]
        public void Retrive_all_doctors()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            IDoctorService doctorService = SetupSettingsDoctorService(scope);

            //Act
            List<DoctorForCreatingAppointmentDto> allDoctors = doctorService.GetAllDoctorsForCreatingAppointment();

            //Assert
            Assert.NotNull(allDoctors);
        }

        [Fact]
        public void Appointments_validation_success_all()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = ""; // Sve prazno
            checkAvailableAppontmentDto.toDate = "";
            checkAvailableAppontmentDto.fromTime = "";
            checkAvailableAppontmentDto.toTime = "";
            checkAvailableAppontmentDto.prefer = "";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_some()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = ""; // Prazan parametar
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "Koka nije snela jaje xd"; // Random kobasica
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_fromDate()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_toDate()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_fromTime()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_toTime()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_prefer()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_personID()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = -1;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_selectedDoctorID()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = -1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_fromDate_parse()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "adsvdsdfv";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_toDate_parse()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "adsvdsdfv";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_fromTime_parse()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "asdfasdf";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_toTime_parse()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "sdafdf";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_from_to_date()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-15"; // od do greska
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_past_from_date()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2020-12-15";
            checkAvailableAppontmentDto.toDate = "2020-12-17";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Appointments_available_validation_success_none()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);
            CheckAvailableAppontmentDto checkAvailableAppontmentDto = new CheckAvailableAppontmentDto();
            checkAvailableAppontmentDto.fromDate = "2023-12-12";
            checkAvailableAppontmentDto.toDate = "2023-12-13";
            checkAvailableAppontmentDto.fromTime = "23:19";
            checkAvailableAppontmentDto.toTime = "23:40";
            checkAvailableAppontmentDto.prefer = "doctor";
            checkAvailableAppontmentDto.selectedDoctorID = 1;
            checkAvailableAppontmentDto.personID = 9;

            //Act
            List<AppointmentsAvailableForCreatingAppointment> result = appointmentService.GetAllAvailableAppointmentsForCreatingAppointment(checkAvailableAppontmentDto);


            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Appointments_creating_validation_success_none()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);

            CustomAppointmentForCreatingDto checkAppointment = new CustomAppointmentForCreatingDto();
            checkAppointment.DoctorID = "1";
            checkAppointment.PersonID = "14";
            checkAppointment.CreateDate = "2023-12-13 08:00";

            //Act
            bool made = appointmentService.CreateCustomAppointment(checkAppointment);

            //Assert
            Assert.True(made);
        }

        [Fact]
        public void Appointments_creating_validation_success_exists()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);

            CustomAppointmentForCreatingDto checkAppointment = new CustomAppointmentForCreatingDto();
            checkAppointment.DoctorID = "1";
            checkAppointment.PersonID = "14";
            checkAppointment.CreateDate = "2030-12-13 07:59";

            //Act
            bool made = appointmentService.CreateCustomAppointment(checkAppointment);

            //Assert
            Assert.False(made);
        }

        [Fact]
        public void Appointments_creating_validation_success_doctorID()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);

            CustomAppointmentForCreatingDto checkAppointment = new CustomAppointmentForCreatingDto();
            checkAppointment.DoctorID = "-1";
            checkAppointment.PersonID = "14";
            checkAppointment.CreateDate = "2023-12-13 08:00";

            //Act
            bool made = appointmentService.CreateCustomAppointment(checkAppointment);

            //Assert
            Assert.False(made);
        }

        [Fact]
        public void Appointments_creating_validation_success_personID()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);

            CustomAppointmentForCreatingDto checkAppointment = new CustomAppointmentForCreatingDto();
            checkAppointment.DoctorID = "1";
            checkAppointment.PersonID = "101";
            checkAppointment.CreateDate = "2023-12-13 08:00";

            //Act
            bool made = appointmentService.CreateCustomAppointment(checkAppointment);

            //Assert
            Assert.False(made);
        }

        [Fact]
        public void Appointments_creating_validation_success_createDate_parse()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);

            CustomAppointmentForCreatingDto checkAppointment = new CustomAppointmentForCreatingDto();
            checkAppointment.DoctorID = "1";
            checkAppointment.PersonID = "101";
            checkAppointment.CreateDate = "2022-12-13 08:00asdfsd";

            //Act
            bool made = appointmentService.CreateCustomAppointment(checkAppointment);

            //Assert
            Assert.False(made);
        }

        [Fact]
        public void Appointments_creating_validation_success_createDate_past()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService appointmentService = SetupSettingsPublicAppointmentService(scope);

            CustomAppointmentForCreatingDto checkAppointment = new CustomAppointmentForCreatingDto();
            checkAppointment.DoctorID = "1";
            checkAppointment.PersonID = "101";
            checkAppointment.CreateDate = "2020-12-13 08:00";

            //Act
            bool made = appointmentService.CreateCustomAppointment(checkAppointment);

            //Assert
            Assert.False(made);
        }
    }
}
