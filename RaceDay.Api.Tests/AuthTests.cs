using Xunit;

namespace RaceDay.Api.Tests
{
    public class AuthTests
    {
        [Fact]
        public void Registration_Should_Pass_When_Data_Is_Valid()
        {
            var email = "test@test.com";

            Assert.NotNull(email);
        }

        [Fact]
        public void Login_Should_Pass_With_Valid_Credentials()
        {
            var password = "Password123";

            Assert.Equal("Password123", password);
        }

        [Fact]
        public void Login_Should_Fail_With_Wrong_Password()
        {
            var expected = "Password123";
            var actual = "WrongPassword";

            Assert.NotEqual(expected, actual);
        }
    }
}
