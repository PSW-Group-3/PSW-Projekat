using System;
using System.Collections.Generic;
using System.Linq;
using HospitalAPI;
using HospitalAPI.Controllers.PublicApp;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace HospitalTests.Integration
{
    public class ExaminationSearchTests : BaseIntegrationTest
    {
        private IExaminationService _examinationService;

        public ExaminationSearchTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { }


        private static ExaminationController SetupSettingsController(IServiceScope scope)
        {

            return new ExaminationController(scope.ServiceProvider.GetRequiredService<IExaminationService>(),
                                            scope.ServiceProvider.GetRequiredService<IAppointmentService>(),
                                            scope.ServiceProvider.GetRequiredService<ISymptomService>(),
                                            scope.ServiceProvider.GetRequiredService<IMedicineService>());
        }

        [Fact]
        public void Search_examination_reports()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            //Act
            string searchWord = "kost";
            string searchWord2 = "Enteritis salmonellosa";
            var result = ((OkObjectResult)controller.GetAllExaminationsBySearchReport(searchWord, 2));
            var result2 = ((OkObjectResult)controller.GetAllExaminationsBySearchReport(searchWord2, 2));

            List<ExaminationDto> examinationDtos =(List<ExaminationDto>) result.Value;
            List<ExaminationDto> examinationDtos2 = (List<ExaminationDto>)result2.Value;

            //Assert
            foreach (ExaminationDto exam in examinationDtos)
            {
                Assert.NotNull(exam.Report);
                Assert.Contains(searchWord, exam.Report);
                Assert.DoesNotContain("\"", exam.Report);
            }

            foreach (ExaminationDto exam in examinationDtos2)
            {
                Assert.NotNull(exam.Report);
                Assert.Contains(searchWord2, exam.Report);
                Assert.Contains("\"", exam.Report);
            }

            Assert.NotNull(result);
            Assert.NotNull(result2);
            Assert.NotEqual(0, result.Value);
            Assert.Equal(200, result.StatusCode);
        }

    }
}
