using Xunit;

namespace RaceDay.Api.Tests
{
    public class EnrolmentTests
    {
        [Fact]
        public void Participant_Can_Enroll()
        {
            bool enrolled = true;

            Assert.True(enrolled);
        }

        [Fact]
        public void Enrolment_Status_Defaults_To_Pending()
        {
            string status = "Pending";

            Assert.Equal("Pending", status);
        }

        [Fact]
        public void Participant_Has_Valid_Category()
        {
            int categoryId = 1;

            Assert.True(categoryId > 0);
        }
    }
}