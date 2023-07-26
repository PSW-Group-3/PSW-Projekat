using System.Collections.Generic;
using HospitalAPI;
using HospitalAPI.Controllers;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;


namespace HospitalTests.Integration
{
    public class RoomTest : BaseIntegrationTest
    {
        public RoomTest(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        private static RoomsController SetupSettingsController(IServiceScope scope)
        {
            return new RoomsController(scope.ServiceProvider.GetRequiredService<IRoomService>()
                                         
               );
        }

        [Fact]
        public void Beds_For_Appropriate_Room()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkObjectResult)controller.GetAllBedsByRoom(2))?.Value as IEnumerable<BedDto>;

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }

        [Fact]
        public void Medicines_For_Storage_Room()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkObjectResult)controller.GetAllStorageMedicnes())?.Value as IEnumerable<Medicine>;

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
