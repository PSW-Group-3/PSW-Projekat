using HospitalAPI;
using HospitalAPI.Controllers.PrivateApp;
using HospitalAPI.DTO;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Shouldly;
using System.Collections.Generic;
using Xunit;

namespace HospitalTests.Integration
{
    public class BloodConsumptionTest : BaseIntegrationTest
    {
        public BloodConsumptionTest(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static BloodConsumptionController SetupSettingsController(IServiceScope scope)
        {
            return new BloodConsumptionController(scope.ServiceProvider.GetRequiredService<IBloodConsumptionService>(), scope.ServiceProvider.GetRequiredService<IDoctorService>(), scope.ServiceProvider.GetRequiredService<IRoomService>());
        }

        [Fact]
        public void Check_if_can_create_blood_consumption()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            
            BloodConsumptionDTO testCase = new BloodConsumptionDTO()
            {
                Id  = 0,
                Blood = new Blood 
                {Id=0, Deleted=false,
                    BloodType = BloodType.AMinus,
                    Quantity = 10
                },
                Purpose ="Potrebno za novu operaciju",
                DoctorId = 5

            };

            var result = ((OkObjectResult)controller.Create(testCase))?.Value as BloodConsumptionDTO;
            Assert.NotNull(result);
            result.ShouldBe(testCase);
            Assert.Equal(result.DoctorId, testCase.DoctorId);
        }

        [Fact]
        public void Reduce_the_number_of_blood_units() {

            var stubRepo = new Mock<IRoomRepository>();
            var stubBlod = new Mock<IBloodRepository>();
            var rooms = new List<Room>();
            var bloods = new List<Blood>();
            Blood b = new Blood
            {
                BloodType = 0,
                Deleted = false,
                Id = 1,
                Quantity = 100
            };
            
            bloods.Add(b);
            Room r1 = new Room
            {
                Id = 4,
                Number = new RoomName("Storage"),
                Beds = new List<Bed>(),
                Floor = new RoomFloor(1),
                Deleted = false,
                Medicines = new List<Medicine>(),
                RoomType = 0,
                Bloods = bloods
                 
            };
           
            rooms.Add(r1);

            stubBlod.Setup(m => m.GetAll()).Returns(bloods);
            stubBlod.Setup(m => m.GetAll()).Returns(bloods);
            stubRepo.Setup(m => m.GetAll()).Returns(rooms);
            stubRepo.Setup(m => m.GetById(4)).Returns(r1);
            stubRepo.Setup(m => m.GetBloods()).Returns(bloods);


            RoomService roomService = new RoomService(stubRepo.Object,stubBlod.Object);
            Blood blodTest = new Blood {
                BloodType = 0,
                Deleted = false,
                Id = 1,
                Quantity = 50
            };
            bool result = roomService.ReduceBloodCount(blodTest);
            Assert.Equal(result, true);
        }


        [Fact]
        public void Not_enough_units_in_the_system()
        {

            var stubRepo = new Mock<IRoomRepository>();
            var stubBlod = new Mock<IBloodRepository>();
            var rooms = new List<Room>();
            var bloods = new List<Blood>();
            Blood b = new Blood
            {
                BloodType = 0,
                Deleted = false,
                Id = 1,
                Quantity = 25
            };

            bloods.Add(b);
            Room r1 = new Room
            {
                Id = 4,
                Number = new RoomName("Storage"),
                Beds = new List<Bed>(),
                Floor = new RoomFloor(1),
                Deleted = false,
                Medicines = new List<Medicine>(),
                RoomType = 0,
                Bloods = bloods

            };

            rooms.Add(r1);

            stubBlod.Setup(m => m.GetAll()).Returns(bloods);
            stubBlod.Setup(m => m.GetAll()).Returns(bloods);
            stubRepo.Setup(m => m.GetAll()).Returns(rooms);
            stubRepo.Setup(m => m.GetById(4)).Returns(r1);
            stubRepo.Setup(m => m.GetBloods()).Returns(bloods);


            RoomService roomService = new RoomService(stubRepo.Object, stubBlod.Object);
            Blood blodTest = new Blood
            {
                BloodType = 0,
                Deleted = false,
                Id = 1,
                Quantity = 100
            };
            bool result = roomService.ReduceBloodCount(blodTest);
            Assert.Equal(result, false);
        }

    }
}
