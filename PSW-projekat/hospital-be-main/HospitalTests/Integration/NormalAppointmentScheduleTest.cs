using HospitalAPI;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using HospitalTests.Setup;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using Xunit;

namespace HospitalTests.Integration
{
    public class NormalAppointmentScheduleTest : BaseIntegrationTest
    {
        public NormalAppointmentScheduleTest(TestDatabaseFactory<Startup> factory) : base(factory) { }

        private static AppointmentService SetupSettingsAppointmentService(IServiceScope scope)
        {
            return new AppointmentService(
                scope.ServiceProvider.GetRequiredService<IAppointmentRepository>(),
                scope.ServiceProvider.GetRequiredService<IWorkingDayRepository>(),
                scope.ServiceProvider.GetRequiredService<IDoctorRepository>(),
                scope.ServiceProvider.GetRequiredService<IPatientRepository>()
                );
        }

        [Fact]
        public void Get_free_appointments_for_doctor()
        {
            //Arrange
            using var scope = Factory.Services.CreateScope();
            AppointmentService service = SetupSettingsAppointmentService(scope);
            int doctorId = 1;
            DateTime newDateTime = new DateTime(2022, 12, 10, 9, 20, 0);
            DateAndDoctorForNewAppointmentDto dto = new DateAndDoctorForNewAppointmentDto() { DoctorId = 1, ScheduledDate = newDateTime };

            //Act
            List<string> appointmentTimes = service.GetFreeAppointmentsForDoctor(1, newDateTime);

            //Assert
            Assert.Equal(6, appointmentTimes.Count);
        }
    }
}
