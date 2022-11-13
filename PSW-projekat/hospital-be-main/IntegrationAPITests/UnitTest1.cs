using System;
using Xunit;
using IntegrationAPI;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service;
using IntegrationLibrary.Core.BloodBankConnection;
using IntegrationLibrary.Core.Repository.BloodBanks;
using IntegrationLibrary.Core.Service.BloodBanks;
using IntegrationLibrary.Settings;
using Moq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace IntegrationAPITests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var stubRepo = new Mock<IBloodBankRepository>();
            var bloodbanks = new List<BloodBank>();
            BloodBank b1 = new BloodBank
            {
                Id = 1,
                Name = "aa",
                Email = "asdasd@gmail.com",
                Password = "asdsadsdadas",
                ApiKey = "sadfasdads",
                ServerAddress = "https://www.messenger.com/t/100001603572170",
                AccountStatus = AccountStatus.ACTIVE
            };
            BloodBank b2 = new BloodBank
            {
                Id = 2,
                Name = "aa",
                Email = "asdasd@gmail.com",
                Password = "asdsadsdadas",
                ApiKey = "sadfasdads",
                ServerAddress = "https://www.messenger.com/t/100001603572170",
                AccountStatus = AccountStatus.ACTIVE
            };
            bloodbanks.Add(b1);
            bloodbanks.Add(b2);

            stubRepo.Setup(m => m.GetAll()).Returns(bloodbanks);
            stubRepo.Setup(m => m.GetById(1)).Returns(b1);


            BloodBankService bloodBankService = new BloodBankService(stubRepo.Object, new EmailService(), new BloodBankHTTPConnection(), new RabbitMQService() );

            BloodBank t =  bloodBankService.GetById(2);
            Assert.Null(t);
        }
    }
}
