using HospitalAPI;
using HospitalLibrary.Core.Model;
using HospitalTests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.Unit
{
    public class AppointmentEntityTests : BaseIntegrationTest
    {
        public AppointmentEntityTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        [Fact]
        public void Appointment_constructor_empty()
        {
            // Arrange

            // Act
            Appointment appointment = new Appointment();

            // Assert
            Assert.NotNull(appointment);
        }

        [Fact]
        public void Appointment_constructor_past_date_throws_exception()
        {
            // Arrange
            DateTime newDate = DateTime.Now.AddDays(-1);
            Patient patient = new Patient();
            Doctor doctor = new Doctor();

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Appointment(0, false, patient, doctor, newDate, null)
            );
        }

        [Fact]
        public void Appointment_constructor_success()
        {
            // Arrange
            DateTime newDate = DateTime.Now.AddDays(1);
            Patient patient = new Patient();
            Doctor doctor = new Doctor();

            // Act
            Appointment appointment = new Appointment(0, false, patient, doctor, newDate, null);

            // Assert
            Assert.NotNull(appointment);
        }
    }
}
