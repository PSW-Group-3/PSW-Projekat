using HospitalAPI;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalTests.Setup;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.Unit
{
    public class DoctorEntityTests : BaseIntegrationTest
    {
        public DoctorEntityTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        [Fact]
        public void Doctor_constructor_empty()
        {
            // Arrange

            // Act
            Doctor doctor = new Doctor();

            // Assert
            Assert.NotNull(doctor);
        }

        [Fact]
        public void Doctor_constructor_person_throws_exception()
        {
            // Arrange
            Specialization specialization = Specialization.cardiology;
            Person person = null;
            ICollection<Patient> patients = null;
            ICollection<DoctorsCouncil> councils = null;
            ICollection<DoctorSchedule> doctorSchedule = new Collection<DoctorSchedule>();
            doctorSchedule.Add(new DoctorSchedule());

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Doctor(specialization, person, patients, councils, doctorSchedule));
        }

        [Fact]
        public void Doctor_constructor_success()
        {
            // Arrange
            Specialization specialization = Specialization.cardiology;
            Person person = new Person();
            ICollection<Patient> patients = null;
            ICollection<DoctorsCouncil> councils = null;
            ICollection<DoctorSchedule> doctorSchedule = new Collection<DoctorSchedule>();
            doctorSchedule.Add(new DoctorSchedule());

            // Act
            Doctor doctor = new Doctor(specialization, person, patients, councils, doctorSchedule);

            // Assert
            Assert.NotNull(doctor);
        }
    }
}