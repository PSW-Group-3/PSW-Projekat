using IntegrationLibrary.Core.Model;
using Shouldly;
using Xunit;

namespace IntegrationAPITests.Tests
{
    public class BloodBankTests
    {
        [Theory]
        [InlineData("Bloody Mary", "BloodyMary@gmail.com", "https://medium.com/agilix/entity-framework-core-system-invalidoperationexception-4720d27ee87d")]
        [InlineData("Bloody Jack", "BloodyJack@gmail.com", "https://medium.com/agilix/entity-framework-core-system-invalidoperationexception-4720d27ee87d")]
        [InlineData("Bloody Sarah", "BloodySarah@gmail.com", "https://medium.com/agilix/entity-framework-core-system-invalidoperationexception-4720d27ee87d")]
        public void Successfull_BloodBank_creation_test(string name, string email, string address)
        {
            new BloodBank(name, email, address).ShouldNotBeNull();
        }

        [Theory]
        [InlineData("Bloody Mary", "BloodyMary@", "https://medium.com/agilix/entity-framework-core-system-invalidoperationexception-4720d27ee87d")]
        [InlineData("Bloody Jack", "@gmail.com", "https://medium.com/agilix/entity-framework-core-system-invalidoperationexception-4720d27ee87d")]
        [InlineData("Bloody Sarah", "", "https://medium.com/agilix/entity-framework-core-system-invalidoperationexception-4720d27ee87d")]
        [InlineData("Bloody Sarah", "BloodySarah@gmail.com", "")]
        [InlineData("", "", "")]
        public void Failed_BloodBank_creation_test(string name, string email, string address)
        {
            BloodBank bloodBank = null;
            try
            {
                bloodBank = new BloodBank(name, email, address);
            } 
            catch
            {
                return;
            }
            bloodBank.ShouldBeNull();
        }
    }
}
