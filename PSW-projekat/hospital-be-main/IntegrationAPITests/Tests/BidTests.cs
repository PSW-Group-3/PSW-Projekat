using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPI.DTO;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Service;
using IntegrationLibrary.Core.Service.BloodBanks;
using IntegrationLibrary.Core.Service.Tenders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationAPITests.Tests
{
    public class BidTests : BaseIntegrationTest
    {
        public BidTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static BidController SetupSettingsBidController(IServiceScope scope)
        {
            return new BidController(scope.ServiceProvider.GetRequiredService<IBidService>());
        }
        
      
        
        [Fact]
        public void Create_Bid_For_Tender() 
        {
            using var scope = Factory.Services.CreateScope();
            BidController bidController = SetupSettingsBidController(scope);
            //BloodBanksController bloodBanksController = SetupSettingsBloodBankController(scope);
            Offer offer1 = new Offer()
            {
                Price = 100,
                Quantity = 5,
                BloodType = BloodType.ABN
            };

            Offer offer2 = new Offer()
            {
                Price = 200,
                Quantity = 6,
                BloodType = BloodType.ABP
            };
            Offer offer3 = new Offer()
            {
                Price = 300,
                Quantity = 7,
                BloodType = BloodType.AN
            };
            List<Offer> offerList = new List<Offer>();
                offerList.Add(offer1);
            offerList.Add(offer2);
            offerList.Add(offer3);
            Bid bid = new Bid()
            {
                DeliveryDate = DateTime.Now,
               Offers = offerList,
                TenderOfBidId = 1,
                BloodBankId = 1,
                Status = BidStatus.WAITING
            };

            bidController.Create(bid);
            var bids = (ObjectResult)bidController.GetAll();
            Assert.NotNull((((ObjectResult)bidController.GetAll()) .Value as List<Bid>)[0]);
        }

        [Fact]

        public void Checking_for_bids_update()
        {
            using var scope = Factory.Services.CreateScope();
            BidController bidController = SetupSettingsBidController(scope);
            List<Bid> bids = ((ObjectResult)bidController.GetAll()).Value as List<Bid>;
            Bid winner = bids[0];   
            List<Bid> losers = ((ObjectResult)bidController.GetByTenderId(winner.TenderOfBidId)).Value as List<Bid>;

            bidController.SelectWinner(winner);

            Assert.True(Iterate_truogh_the_losers(winner));
        }

        private bool Iterate_truogh_the_losers(Bid winner)
        {
            using var scope = Factory.Services.CreateScope();
            BidController bidController = SetupSettingsBidController(scope);
            List<Bid> losers = ((ObjectResult)bidController.GetByTenderId(winner.TenderOfBidId)).Value as List<Bid>;
            int winner_counter = 0;
            foreach(Bid loser in losers)
            {
                if(loser.Status == winner.Status){ winner_counter++;}
            }
            int loser_counter = 0;
            foreach (Bid loser in losers)
            {
                if(loser.Status == BidStatus.LOST){loser_counter++;}
            }
            return (winner_counter == 1 && loser_counter == losers.Count - 1);
        }

        
    }
}
