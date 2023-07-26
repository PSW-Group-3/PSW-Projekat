using IntegrationLibrary.Core.Model;
using Shouldly;
using System;
using Xunit;

namespace IntegrationAPITests.Tests
{
    public class EmailTests
    {
        [Theory]
        [InlineData("halidpasanovic@gmail.com")]
        [InlineData("asdsda.asdsda@gmail.com")]
        [InlineData("a.a.sd.s.ad@saddas.rs")]
        public void Successfull_email_creation_test(string EmailAddress)
        {
            new Email(EmailAddress);
        }

        [Theory]
        [InlineData("@gmail.com")]
        [InlineData("asdsda.asdsda@")]
        [InlineData("@")]
        [InlineData("")]
        public void Fail_email_creation_test(string EmailAddress)
        {
            Email email = null;
            try
            {
                email = new Email(EmailAddress);
            }
            catch (ArgumentException)
            {
                return;
            }
            email.ShouldBeNull();
        }
    }
}
