using HospitalAPI;
using HospitalAPI.Controllers.PrivateApp;
using HospitalAPI.DTO;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service.CouncilOfDoctors;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace HospitalTests.Integration
{
    public class CouncilOfDoctorsTests : BaseIntegrationTest
    {

        public CouncilOfDoctorsTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static CouncilOfDoctorsController SetupSettingsController(IServiceScope scope)
        {
            return new CouncilOfDoctorsController(scope.ServiceProvider.GetRequiredService<ICouncilOfDoctorsService>());
        }


        [Fact]
        public void Check_if_can_create_doctors_council()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);
            List<Specialization> specialization = new List<Specialization>();
            specialization.Add(Specialization.general);
            specialization.Add(Specialization.neurology);
            ICollection<DoctorDto> doctors = new List<DoctorDto>();

            DoctorCouncilDTO testCase = new DoctorCouncilDTO()
            {
                 Id= 0,
                DoctorId= 1,
                Topic= "Tema",
                Doctors = doctors,
                Specializations=specialization,
                Start=new DateTime(2022,12,12,7,42,0),
                End= new DateTime(2022, 12, 15, 7, 42, 0),
                Duration= 0
            };

            var result = ((OkObjectResult)controller.Create(testCase))?.Value as DoctorCouncilDTO;
            Assert.NotNull(result);
            result.ShouldBe(testCase);
            Assert.Equal(result.DoctorId, testCase.DoctorId);
        }

        [Fact]
        public void Get_all_consiliums()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            var result = ((OkObjectResult)controller.GetAll())?.Value as IEnumerable<DoctorsCouncilDto>;

            

            //Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
           
            
        }

    }
}
