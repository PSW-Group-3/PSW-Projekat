using IntegrationAPI;
using IntegrationAPI.Controllers;
using IntegrationAPITests.Setup;
using IntegrationLibrary.Core.Service.Tenders;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationAPITests.Tests
{
    public class BidTests : BaseIntegrationTest
    {
        public BidTests(TestDatabaseFactory<Startup> factory) : base(factory)
        {
        }

        private static TenderController SetupSettingsBidController(IServiceScope scope)
        {
            return new TenderController(scope.ServiceProvider.GetRequiredService<ITenderService>());
        }
        
      
        
        //[Fact]
        //public void Create_Bid_For_Tender() 
        //{
        //    using var scope = Factory.Services.CreateScope();
        //    BidController bidController = SetupSettingsBidController(scope);
        //    //BloodBanksController bloodBanksController = SetupSettingsBloodBankController(scope);

        //    Bid bid = new Bid()
        //    {
        //        DeliveryDate = DateTime.Now,
        //        Price = 2000,
        //        TenderOfBidId = 1,
        //        BloodBankId = 1,
        //        Status = BidStatus.WAITING
        //    };

        //    bidController.Create(bid);
        //    var bids = (ObjectResult)bidController.GetAll();
        //    Assert.NotNull((((ObjectResult)bidController.GetAll()) .Value as List<Bid>)[0]);
        //}

        //[Fact]

        //public void Checking_for_bids_update()
        //{
        //    using var scope = Factory.Services.CreateScope();
        //    BidController bidController = SetupSettingsBidController(scope);
        //    List<Bid> bids = ((ObjectResult)bidController.GetAll()).Value as List<Bid>;
        //    Bid winner = bids[0];   
        //    List<Bid> losers = ((ObjectResult)bidController.GetByTenderId(winner.TenderOfBidId)).Value as List<Bid>;

        //    bidController.SelectWinner(winner);

        //    Assert.True(Iterate_truogh_the_losers(winner));
        //}

        //private bool Iterate_truogh_the_losers(Bid winner)
        //{
        //    using var scope = Factory.Services.CreateScope();
        //    BidController bidController = SetupSettingsBidController(scope);
        //    List<Bid> losers = ((ObjectResult)bidController.GetByTenderId(winner.TenderOfBidId)).Value as List<Bid>;
        //    int winner_counter = 0;
        //    foreach(Bid loser in losers)
        //    {
        //        if(loser.Status == winner.Status){ winner_counter++;}
        //    }
        //    int loser_counter = 0;
        //    foreach (Bid loser in losers)
        //    {
        //        if(loser.Status == BidStatus.LOST){loser_counter++;}
        //    }
        //    return (winner_counter == 1 && loser_counter == losers.Count - 1);
        //}

        
    }
}
