using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Repository.CRUD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Repository.Newses
{
    public interface INewsRepository : ICRUDRepository<News>
    {
        public IEnumerable<News> GetAllPending();
    }
}
