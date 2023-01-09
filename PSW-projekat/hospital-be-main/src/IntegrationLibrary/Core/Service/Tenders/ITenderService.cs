using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Service.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.Tenders
{
    public interface ITenderService : ICRUDService<Tender>
    {
        public IEnumerable<Tender> GetAllOpen();

        public void CloseTenderWithWinner(int tenderID, int winningBidId);

        public void BidOnTender(int tenderID, Bid bid);
    }
}
