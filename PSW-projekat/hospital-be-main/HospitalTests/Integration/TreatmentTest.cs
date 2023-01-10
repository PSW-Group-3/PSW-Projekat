using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HospitalAPI;
using HospitalAPI.Controllers.PublicApp;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HospitalTests.Integration
{
    public class TreatmentTest : BaseIntegrationTest
    {

        private ITreatmentService _treatmentService;
        private static IPatientService _patientService;
        private readonly ITherapyService _therapyService;
        private readonly IMedicineService _medicineService;
        private readonly IBloodService _bloodService;
        private readonly IRoomService _roomService;

        public TreatmentTest(TestDatabaseFactory<Startup> factory) : base(factory)
        { }


        private static TreatmentController SetupSettingsController(IServiceScope scope)
        {
            
            return new TreatmentController(scope.ServiceProvider.GetRequiredService<IPatientService>(),
                                           scope.ServiceProvider.GetRequiredService<ITreatmentService>(),
                                           scope.ServiceProvider.GetRequiredService<IRoomService>(),
                                           scope.ServiceProvider.GetRequiredService<IBedService>(),
                                           scope.ServiceProvider.GetRequiredService<IBloodService>(),
                                           scope.ServiceProvider.GetRequiredService<IMedicineService>(),
                                           scope.ServiceProvider.GetRequiredService<ITherapyService>()
               );
        }
        
        [Fact]
        public void Find_single_treatment()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);
            //Act
            var result = ((OkObjectResult)controller.GetById(1))?.Value as TreatmentDto;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Admission_patient_to_treatment()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            PatientDto patientDto = new PatientDto(6, "Tara", "Taric", "tara@gmail.com", Role.patient);
            Medicine medicine = new Medicine(2, false, "Aspirin", 20);
            Blood blood = new Blood(1, false, BloodType.APlus, 2);
            //a id u terapiji?
            Therapy therapy = new Therapy(medicine, blood, 1, 1);

            RoomDto roomDto = new RoomDto(1, new RoomName("101A"), new RoomFloor(1), RoomType.rehabilitationRoom, null);

            //Act
            TreatmentDto testCase = new TreatmentDto()
            {
                Patient = patientDto,
                ReasonForDischarge = "",
                ReasonForAdmission = "Prelom ruke",
                DateAdmission = DateTime.Now,
                DateDischarge = DateTime.Now,
                RoomDto = roomDto,
                Therapy = therapy

            };

            var result = ((OkResult)controller.Create(testCase));

            //Assert
            Assert.NotNull(result);
            Assert.NotNull(testCase.Therapy);
        }

        
        [Fact]
        public void Find_treatment_to_close()
        {
             //Arrange
             using var scope = Factory.Services.CreateScope();
             var controller = SetupSettingsController(scope);

            //Act
            PatientDto patientDto = new PatientDto(6, "Tara", "Taric", "tara@gmail.com", Role.patient);
            Medicine medicine = new Medicine(2, false, "Aspirin", 20);
            Blood blood = new Blood(1, false, BloodType.APlus, 2);
            Therapy therapy = new Therapy(medicine, blood, 1, 1);
            RoomDto roomDto = new RoomDto(1, new RoomName("101A"), new RoomFloor(1), RoomType.rehabilitationRoom, null);

            TreatmentDto testCase = new TreatmentDto()
            {
                Id = 2,
                Patient = patientDto,
                ReasonForDischarge = "",
                ReasonForAdmission = "Glavobolja",
                DateAdmission = new DateTime(2022, 11, 24, 08, 01, 0, 0),
                DateDischarge = new DateTime(2022, 11, 24, 01, 02, 23, 678),
                RoomDto = roomDto,
                Therapy = therapy
            };
            testCase.DateDischarge = new DateTime();
            testCase.ReasonForDischarge = "Laksse mu je sad";
            
            var result = ((FileContentResult)controller.Update(2,testCase));

            //Assert
            Assert.NotNull(result);
            /*
            Assert.Equal(2, result.Id);
            Assert.Equal(testCase.Patient, result.Patient);
            Assert.NotNull(result.Patient);
            */
         
        }


    }
}
