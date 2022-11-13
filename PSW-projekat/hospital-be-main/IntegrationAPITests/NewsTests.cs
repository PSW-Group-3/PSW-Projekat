using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.Newses;
using IntegrationLibrary.Core.Service.Newses;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationAPITests
{
    public class NewsTests
    {
        [Fact]
        public void Test1()
        {
            var stubRepo = new Mock<INewsRepository>();
            var newses = new List<News>();
            News n1 = new News()
            {
                Id = 1,
                Name = "Blood donation 1",
                DateTime = new DateTime(2022, 01, 01, 9, 15, 0),
                BloodBankId = 1,
            };
            newses.Add(n1);
            stubRepo.Setup(m => m.GetAll()).Returns(newses);
            stubRepo.Setup(m => m.GetById(1)).Returns(n1);

            NewsService newsService = new NewsService(stubRepo.Object);

            News n = newsService.GetById(1);
            Assert.NotNull(n);
        }
    }
}
