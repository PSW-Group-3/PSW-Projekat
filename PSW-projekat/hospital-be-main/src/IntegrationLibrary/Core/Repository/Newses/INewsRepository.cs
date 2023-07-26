using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.CRUD;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Repository.Newses
{
    public interface INewsRepository : ICRUDRepository<News>
    {
        public IEnumerable<News> GetAllPending();
    }
}
