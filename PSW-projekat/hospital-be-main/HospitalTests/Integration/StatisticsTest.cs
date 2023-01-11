using HospitalAPI;
using HospitalAPI.Controllers.PrivateApp;
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
    public class StatisticsTest : IClassFixture<StatisticsTestDatabaseFactory<Startup>>
    {
        protected StatisticsTestDatabaseFactory<Startup> StatisticsFactory { get; }

        public StatisticsTest(StatisticsTestDatabaseFactory<Startup> factory)
        {
            StatisticsFactory = factory;
        }

        private static StatisticsController SetupSettingsController(IServiceScope scope)
        {
            return new StatisticsController(scope.ServiceProvider.GetRequiredService<StatisticsService>(), scope.ServiceProvider.GetRequiredService<SchedulingStatisticsService>());
        }

        [Fact]
        public void Get_correct_statistics()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var statistics = ((OkObjectResult)controller.GetStatistics())?.Value as StatisticsDto;

            //Assert 
            Assert.True(statistics.BloodtypePopularity.ContainsKey("A+"));
            Assert.True(statistics.NumberOfFemalesPerAgeGroup[1] == 1);
            Assert.True(statistics.AllergyPopularity.ContainsKey("Polen"));
            Assert.True(statistics.DoctorsAgeGroupDistribution.ContainsKey("Milan Milovanovic"));

        }

        [Fact]
        public void Get_correct_scheduling_events_statistics()
        {
            //Arrange
            using var scope = StatisticsFactory.Services.CreateScope();
            var controller = SetupSettingsController(scope);
            var dto1 = new NumberOfFinishedAndUnfinishedSchedulingForPatient(1, "Jovana Jovanovic", 3, 1);
            var dto2 = new NumberOfFinishedAndUnfinishedSchedulingForPatient(2, "Nikola Nikolic", 2, 0);

            //Act
            var statistics = ((OkObjectResult)controller.GetAllEventStatistics())?.Value as SchedulingStatisticsDTO;

            //Assert 
            Assert.True(statistics.AvarageSchedulingDuration == 58.8);

            Assert.True(statistics.LinearSchedulingNumber == 3);
            
            Assert.True(statistics.NonlinearSchedulingNumber == 2);

            Assert.True(statistics.FinishedSchedulingsPerDay[0].Equals(1));
            Assert.True(statistics.FinishedSchedulingsPerDay[1].Equals(1));
            Assert.True(statistics.FinishedSchedulingsPerDay[2].Equals(1));
            Assert.True(statistics.FinishedSchedulingsPerDay[3].Equals(1));
            Assert.True(statistics.FinishedSchedulingsPerDay[4].Equals(1));
            Assert.True(statistics.FinishedSchedulingsPerDay[5].Equals(0));
            Assert.True(statistics.FinishedSchedulingsPerDay[6].Equals(0));

            Assert.True(statistics.UnfinishedSchedulingsPerDay[0].Equals(0));
            Assert.True(statistics.UnfinishedSchedulingsPerDay[1].Equals(0));
            Assert.True(statistics.UnfinishedSchedulingsPerDay[2].Equals(0));
            Assert.True(statistics.UnfinishedSchedulingsPerDay[3].Equals(1));
            Assert.True(statistics.UnfinishedSchedulingsPerDay[4].Equals(0));
            Assert.True(statistics.UnfinishedSchedulingsPerDay[5].Equals(0));
            Assert.True(statistics.UnfinishedSchedulingsPerDay[6].Equals(0));

            Assert.True(statistics.AvarageNumberOfStepsForSuccessfulScheduling == 4.8);

            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[0].PatientId.Equals(dto1.PatientId));
            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[0].FullName.Equals(dto1.FullName));
            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[0].NumberOfFinishedSchedulings.Equals(dto1.NumberOfFinishedSchedulings));
            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[0].NumberOfUnfinishedSchedulings.Equals(dto1.NumberOfUnfinishedSchedulings));

            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[1].PatientId.Equals(dto2.PatientId));
            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[1].FullName.Equals(dto2.FullName));
            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[1].NumberOfFinishedSchedulings.Equals(dto2.NumberOfFinishedSchedulings));
            Assert.True(statistics.NumberOfFinishedAndUnfinishedSchedulingForAllPatients[1].NumberOfUnfinishedSchedulings.Equals(dto2.NumberOfUnfinishedSchedulings));

        }
    }
}
