using Xunit;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service;
using IntegrationLibrary.Core.BloodBankConnection;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Service.BloodBanks;
using Moq;
using System.Collections.Generic;
using IntegrationLibrary.Core.HospitalConnection;

namespace IntegrationAPITests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var stubRepo = new Mock<IBloodBankRepository>();
            var bloodbanks = new List<BloodBank>();
            BloodBank b1 = new BloodBank(1, "aa", "asdasd@gmail.com", "asdsadsdadas", "https://www.messenger.com/t/100001603572170", "sadfasdads", "asddsadasdsa", null, AccountStatus.ACTIVE);
            BloodBank b2 = new BloodBank(2, "aa", "asdasd@gmail.com", "asdsadsdadas", "https://www.messenger.com/t/100001603572170", "sadfasdads", "asddsadasdsa", null, AccountStatus.ACTIVE);
            bloodbanks.Add(b1);
            bloodbanks.Add(b2);

            stubRepo.Setup(m => m.GetAll()).Returns(bloodbanks);
            stubRepo.Setup(m => m.GetById(1)).Returns(b1);


            BloodBankService bloodBankService = new BloodBankService(stubRepo.Object, new EmailService(), new BloodBankHTTPConnection(), new RabbitMQService(), new HospitalHTTPConnection());

            BloodBank t =  bloodBankService.GetById(2);
            Assert.Null(t);
        }
        [Fact]
        public void Check_cerdentials_of_blood_bank()
        {
            var rabbit = new RabbitMQService();
            BloodBank b1 = new BloodBank(1, "name", "bloodymary@gmail.com", "password", "https://www.messenger.com/t/100001603572170", "good_key", "asddsadasdsa", null, AccountStatus.ACTIVE);
            List<BloodBank> bloodBanks = new List<BloodBank>();
            bloodBanks.Add(b1);
            string email = "bloodymary@gmail.com";
            string apiKey = "good_key";

            Assert.True(rabbit.checkBloodBankExists(email, bloodBanks) == true);
            Assert.True(rabbit.checkBloodBankApiKey(email, apiKey, bloodBanks) == true);

            Assert.False(rabbit.checkBloodBankExists("bad_email", bloodBanks) == true);
            Assert.False(rabbit.checkBloodBankApiKey(email, "bad_key", bloodBanks) == true);
        }
    }
}
