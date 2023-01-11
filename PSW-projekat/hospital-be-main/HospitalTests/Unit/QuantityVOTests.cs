using HospitalAPI;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Model.Enums;
using HospitalTests.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests.Unit
{
    public class QuantityVOTests : BaseIntegrationTest
    {
        public QuantityVOTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        [Fact]
        public void Wrong_value()
        {
            // Arrange
            int value = -8;

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Quantity(value));
        }

        [Fact]
        public void Valid_quantity()
        {
            // Arrange
            int value = 10;

            // Act
            Quantity quantity = new Quantity(value);

            // Assert
            Assert.NotNull(quantity);
        }

        [Fact]
        public void Is_equal_quantity()
        {
            // Arrange
            Quantity quantity1 = new Quantity(10);
            Quantity quantity = new Quantity(10);

            bool equal = quantity1.IsEquals(quantity.Value);

            // Assert
            Assert.Equal(equal, true);
        }

        [Fact]
        public void Not_equal_quantity()
        {
            // Arrange
            Quantity quantity1 = new Quantity(8);
            Quantity quantity = new Quantity(10);

            bool equal = quantity1.IsEquals(quantity.Value);

            // Assert
            Assert.Equal(equal, false);
        }

        [Fact]
        public void Add_quantity()
        {
            // Arrange
            Quantity quantity = new Quantity(3);
            int value = 2;
            Quantity quantity1 = quantity.Add(value);

            // Assert
            Assert.Equal(quantity1.Value, 5);
        }


        [Fact]
        public void Reduce_quantity()
        {
            // Arrange
            Quantity quantity = new Quantity(3);
            int value = 2;
            Quantity quantity1 = quantity.Reduce(value);

            // Assert
            Assert.Equal(quantity1.Value, 1);
        }


        [Fact]
        public void Valid_council()
        {
            // Arrange
            String topic = "danas";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            start = start.AddHours(2);
            end = end.AddHours(4);
            int duration = 10;
            ICollection<Doctor> doctors = new List<Doctor>();
            ICollection<Specialization> specializations = new List<Specialization>();


            // Act
            DoctorsCouncil quantity = new DoctorsCouncil(topic,start, end, duration, doctors, specializations);

            // Assert
            Assert.NotNull(quantity);
        }

        [Fact]
        public void Wrong_council()
        {
            // Arrange
            String topic = "danas";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            start = start.AddHours(2);
            end = end.AddHours(1);
            int duration = 10;
            ICollection<Doctor> doctors = new List<Doctor>();
            ICollection<Specialization> specializations = new List<Specialization>();


            // Assert
            Assert.Throws<Exception>(() => new DoctorsCouncil(topic, start, end, duration, doctors, specializations));
        }

        [Fact]
        public void Wrong_council_duration()
        {
            // Arrange
            String topic = "danas";
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            start = start.AddHours(2);
            end = end.AddHours(1);
            int duration = -10;
            ICollection<Doctor> doctors = new List<Doctor>();
            ICollection<Specialization> specializations = new List<Specialization>();


            // Assert
            Assert.Throws<Exception>(() => new DoctorsCouncil(topic, start, end, duration, doctors, specializations));
        }
    }
}