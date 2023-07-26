using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.CRUD;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Service.Newses
{
    public interface INewsService : ICRUDService<News>
    {
        IEnumerable<News> GetAllPending();
        IEnumerable<News> GetUpcomingNewsForBloodBank(int bloodbankId);
        IEnumerable<News> GetOldNewsForBloodBank(int bloodBankId);
        
    }
}
