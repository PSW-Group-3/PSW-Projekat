using HospitalAPI;
using HospitalLibrary.Core.Model;
using HospitalTests.Setup;
using System;
using Xunit;

namespace HospitalTests.Unit
{
    public class JmbgVOTests : BaseIntegrationTest
    {
        public JmbgVOTests(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        [Fact]
        public void Wrong_value_length()
        {
            // Arrange
            String value = "123542";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Jmbg(value));
        }

        [Fact]
        public void Value_containts_wrong_characters()
        {
            // Arrange
            String value = "123542a12312b";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Jmbg(value));
        }

        [Fact]
        public void Value_containts_incorrect_date()
        {
            // Arrange
            String value = "1213000800023";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Jmbg(value));
        }
        
        [Fact]
        public void Value_containts_incorrect_control_sum()
        {
            // Arrange
            String value = "2708000800024";

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Jmbg(value));
        }

        [Fact]
        public void Valid_Jmbg()
        {
            // Arrange
            String value = "2503000800030";

            // Act
            Jmbg jmbg = new Jmbg(value);

            // Assert
            Assert.NotNull(jmbg);
        }

    }
}
