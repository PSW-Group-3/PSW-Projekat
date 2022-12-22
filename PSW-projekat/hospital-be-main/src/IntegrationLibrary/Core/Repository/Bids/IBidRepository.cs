using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Repository.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository.Bids
{
    public interface IBidRepository : ICRUDRepository<Bid>
    {
        IEnumerable<Bid> GetByTenderId(int id);
        IEnumerable<Bid> getFromDateToDate(DateTime start, DateTime end);
    }
}
