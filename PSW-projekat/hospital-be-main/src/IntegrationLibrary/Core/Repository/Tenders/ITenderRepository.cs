using IntegrationLibrary.Core.Model.Tender;
using IntegrationLibrary.Core.Repository.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository.Tenders
{
    public interface ITenderRepository : ICRUDRepository<Tender>
    {
        public IEnumerable<Tender> GetAllOpen();
        public List<Tender> GetAllClosed();
    }
}
