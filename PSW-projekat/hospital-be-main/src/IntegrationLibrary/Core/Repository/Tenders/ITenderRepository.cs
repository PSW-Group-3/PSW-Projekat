using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Repository.CRUD;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Repository.Tenders
{
    public interface ITenderRepository : ICRUDRepository<Tender>
    {
        public IEnumerable<Tender> GetAllOpen();
        public List<Tender> GetAllClosed();
    }
}
