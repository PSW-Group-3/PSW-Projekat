using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPITests.Setup;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.Newses;
using IntegrationLibrary.Core.Service.Newses;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using IntegrationLibrary.Core.Service;
using IntegrationLibrary.Core.Repository.BloodBanks;

namespace IntegrationAPITests
{
    public class NewsTests : BaseIntegrationTest

    {
        public NewsTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static NewsController SetupSettingsController(IServiceScope scope)
        {
            return new NewsController(scope.ServiceProvider.GetRequiredService<INewsService>());
        }

        [Fact]
        public void Test1()
        {
           
               var stubRepo = new Mock<INewsRepository>();
               var rabbitMock = new Mock<IRabbitMQService>();
               var bloodRepo = new Mock<IBloodBankRepository>();
               var newses = new List<News>();
               News n1 = new News()
               {
                    Id = 1,
                    Title = "Blood donation 1",
                    Text = "First donation of the year!!",
                    DateTime = new DateTime(2022, 01, 01, 9, 15, 0),
                    BloodBankId = 1,
                };
                newses.Add(n1);
                stubRepo.Setup(m => m.GetAll()).Returns(newses);
                stubRepo.Setup(m => m.GetById(1)).Returns(n1);

                NewsService newsService = new NewsService(stubRepo.Object, rabbitMock.Object
                    , bloodRepo.Object);

                        News n = newsService.GetById(1);
                      Assert.NotNull(n);
        }
        [Fact]
        public void Get_all_pending_news_for_menager()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);

            var res = ((OkObjectResult)controller.GetAllPending()).Value as List<News>;
            Assert.True(Iterate_through_news(res));
        }

        private bool Iterate_through_news(List<News> newses)
        {
            foreach(News news in newses)
            {
                if(news.Status != NewsStatus.PENDING) { return false; }
            }
            return true;
        }

        [Fact]
        public void Menager_change_status_of_news()
        {
            using var scope = Factory.Services.CreateScope();
            var controller = SetupSettingsController(scope);
            News newsForUpdate = (((OkObjectResult)controller.GetAllPending()).Value as List<News>)[0];
            int newsId = newsForUpdate.Id;
            newsForUpdate.Status = NewsStatus.ACTIVATED;

            controller.Update(newsForUpdate);

            Assert.True((((OkObjectResult)controller.GetById(newsId)).Value as News).Status == NewsStatus.ACTIVATED);
        }
        
    }
}
