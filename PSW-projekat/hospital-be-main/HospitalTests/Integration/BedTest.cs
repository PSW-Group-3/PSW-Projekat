using HospitalAPI;
using HospitalAPI.Controllers.PublicApp;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HospitalTests.Integration
{
    public class BedTest : BaseIntegrationTest
    {
        public BedTest(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        private static BedController SetupSettingsController(IServiceScope scope)
        {
            return new BedController(scope.ServiceProvider.GetRequiredService<IBedService>(),
                                     scope.ServiceProvider.GetRequiredService<IPatientService>()   

               );
        }

        [Fact]
        public void Accommodation_patient_in_free_bed()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            PatientDto patientDto = new PatientDto(5, "Mikica", "Mikicovic", "mikica@gmail.com", Role.patient);
            
            BedDto bedCase = new BedDto()
            {
                Id = 2,
                Name = "Dormeo",
                BedState = BedState.free,
                PatientDto = patientDto,
                Quantity = 5
            };

            var result = ((OkResult)controller.Update(2, bedCase));

            //Assert
            Assert.NotNull(result);
            Assert.Equal(2, bedCase.Id);
        }

    }
}
