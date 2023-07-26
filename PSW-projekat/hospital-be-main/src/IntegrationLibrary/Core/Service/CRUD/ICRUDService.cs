using IntegrationLibrary.Core.Model;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Service.CRUD
{
    public interface ICRUDService<TEntity> where TEntity : EntityClass
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
