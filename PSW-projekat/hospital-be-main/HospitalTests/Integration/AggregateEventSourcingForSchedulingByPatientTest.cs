using HospitalAPI;
using HospitalAPI.Controllers.PublicApp;
using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Aggregate;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.Integration
{
    public class AggregateEventSourcingForSchedulingByPatientTest : IClassFixture<StatisticsTestDatabaseFactory<Startup>>
    {
        protected StatisticsTestDatabaseFactory<Startup> StatisticsFactory { get; }

        public AggregateEventSourcingForSchedulingByPatientTest(StatisticsTestDatabaseFactory<Startup> factory)
        {
            StatisticsFactory = factory;
        }

        private static AppointmentSchedulingController SetupSettingsController(IServiceScope scope)
        {
            return new AppointmentSchedulingController(scope.ServiceProvider.GetRequiredService<SchedulingAppointmentEventsRepository>(), scope.ServiceProvider.GetRequiredService<IPatientService>());
        }

        [Fact]
        public void patient_choose_date()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkResult)controller.ChooseAppointmentDate(new AppointmentSchedulingEventDTO (){ Id=1, SelectedItem="12/12/2022"}));

            //Assert 
            Assert.NotNull(result);

        }

        [Fact]
        public void patient_back_to_date()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkResult)controller.BackToAppointmentDateChoosing(1));

            //Assert 
            Assert.NotNull(result);
        }

        [Fact]
        public void create_event_sourcing_aggregate()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkObjectResult)controller.AppointmentSchedulingAggregateStartTime(2))?.Value;

            //Assert 
            Assert.NotNull(result);
        }

        [Fact]
        public void end_event_sourcing_aggregate()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkResult)controller.AppointmentSchedulingAggregateEndTime(1));

            //Assert 
            Assert.NotNull(result);
        }
    }
}
