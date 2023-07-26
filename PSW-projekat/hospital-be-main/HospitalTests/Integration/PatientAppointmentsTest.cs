using HospitalAPI;
using HospitalAPI.Controllers.PublicApp;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace HospitalTests.Integration
{
    public class PatientAppointmentsTest : IClassFixture<StatisticsTestDatabaseFactory<Startup>>
    {
        protected StatisticsTestDatabaseFactory<Startup> StatisticsFactory { get; }

        public PatientAppointmentsTest(StatisticsTestDatabaseFactory<Startup> factory)
        {
            StatisticsFactory = factory;
        }

        private static PublicAppointmentController SetupSettingsController(IServiceScope scope)
        {
            return new PublicAppointmentController(scope.ServiceProvider.GetRequiredService<IAppointmentService>(), scope.ServiceProvider.GetRequiredService<IDoctorService>(), scope.ServiceProvider.GetRequiredService <IPatientService> ());
        }

        [Fact]
        public void Get_cancelled_appointment_for_patient()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkObjectResult)controller.GetPatientAppointments(4))?.Value as List<PatientAppointmentsDto>;

            //Assert 
            Assert.NotNull(result);
            result[0].AppointmentStatus.ShouldBe("Cancelled");

        }

        [Fact]
        public void Get_upcoming_appointment_for_patient()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkObjectResult)controller.GetPatientAppointments(2))?.Value as List<PatientAppointmentsDto>;

            //Assert 
            Assert.NotNull(result);
            result[0].AppointmentStatus.ShouldBe("Upcoming");

        }
    }
}
