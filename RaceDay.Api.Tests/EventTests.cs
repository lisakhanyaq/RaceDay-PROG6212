using Xunit;

namespace RaceDay.Api.Tests
{
    public class EventTests
    {
        [Fact]
        public void Event_Name_Should_Not_Be_Empty()
        {
            string eventName = "Race Day 2026";

            Assert.False(string.IsNullOrWhiteSpace(eventName));
        }

        [Fact]
        public void Event_Distance_Should_Be_Greater_Than_Zero()
        {
            double distance = 10;

            Assert.True(distance > 0);
        }

        [Fact]
        public void Event_Type_Should_Be_Run()
        {
            string eventType = "Run";

            Assert.Equal("Run", eventType);
        }
    }
}
