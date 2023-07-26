using HospitalAPI;
using HospitalLibrary.Core.Model;
using HospitalTests.Setup;
using System;
using Xunit;

namespace HospitalTests.Unit
{
    public class TimeRangeVOTest : BaseIntegrationTest
    {
        public TimeRangeVOTest(TestDatabaseFactory<Startup> factory) : base(factory)
        { }

        [Fact]
        public void TimeVO_constructor_empty()
        {
            // Arrange
            Time time1 = new Time(10, 20);
            Time time2 = new Time(11, 50);

            // Act
            TimeRange timeRange = new TimeRange();

            // Assert
            Assert.NotNull(timeRange);
        }

        [Fact]
        public void TimeRangeVO_constructor_success()
        {
            // Arrange
            Time time1 = new Time(10, 20);
            Time time2 = new Time(11, 50);

            // Act
            TimeRange timeRange = new TimeRange(time1, time2);

            // Assert
            Assert.NotNull(timeRange);
        }

        [Fact]
        public void TimeVO_constructor_throw_exeption_start_time_after_end_time_or_otherwise()
        {
            // Arrange
            Time time1 = new Time(10, 20);
            Time time2 = new Time(11, 50);

            // Act

            // Assert
            Assert.Throws<Exception>(() => new TimeRange(time2, time1));
        }

        [Fact]
        public void TimeVO_constructor_Is_During()
        {
            // Arrange
            Time time1 = new Time(9, 20);
            Time time2 = new Time(11, 20);
            Time time3 = new Time(10, 20);
            TimeRange tr = new TimeRange(time1, time2);

            // Act
            bool isDuring = tr.IsDuring(time3);

            // Assert
            Assert.True(isDuring);
        }
    }
}
