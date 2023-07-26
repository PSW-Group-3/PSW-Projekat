using HospitalAPI;
using HospitalLibrary.Core.Model;
using HospitalTests.Setup;
using System;
using Xunit;

namespace HospitalTests.Unit
{
    public class TimeVOTest : BaseIntegrationTest
    {

        public TimeVOTest(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        [Fact]
        public void TimeVO_constructor_empty()
        {
            // Arrange

            // Act
            Time time = new Time();

            // Assert
            Assert.NotNull(time);
        }

        [Fact]
        public void TimeVO_constructor_success()
        {
            // Arrange
            int Hour = 10;
            int Minute = 20;

            // Act
            Time time = new Time(Hour, Minute);

            // Assert
            Assert.NotNull(time);
        }

        [Fact]
        public void TimeVO_constructor_throw_exeption_hour()
        {
            // Arrange
            int Hour = 25;
            int Minute = 20;

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Time(Hour, Minute));
        }

        [Fact]
        public void TimeVO_constructor_throw_exeption_minute()
        {
            // Arrange
            int Hour = 10;
            int Minute = 63;

            // Act

            // Assert
            Assert.Throws<Exception>(() => new Time(Hour, Minute));
        }

        [Fact]
        public void TimeVO_ToString()
        {
            // Arrange
            int hour = 9;
            int minute = 9;

            // Act
            Time time = new Time(hour, minute);
            string timeString = "09:09";

            // Assert
            Assert.Equal(timeString, time.ToString());
        }

        [Fact]
        public void TimeVO_AddMinutes()
        {
            // Arrange
            int hour = 9;
            int minute = 0;

            // Act
            Time time = new Time(hour, minute);
            time = time.AddMinutes(67);
            // Assert
            Assert.Equal("10:07", time.ToString());
        }

        [Fact]
        public void TimeVO_operator_greater()
        {
            // Arrange
            int hour = 9;
            int minute = 0;

            // Act
            Time time = new Time(hour, minute);
            Time afterTime = time.AddMinutes(67);
            bool isAfter = afterTime > time;

            // Assert
            Assert.True(isAfter);
        }
        [Fact]
        public void TimeVO_operator_lesser()
        {
            // Arrange
            int hour = 9;
            int minute = 0;

            // Act
            Time time = new Time(hour, minute);
            Time afterTime = time.AddMinutes(67);
            bool isAfter = time < afterTime;

            // Assert
            Assert.True(isAfter);
        }
    }
}
