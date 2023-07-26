using IntegrationLibrary.Core.Model;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Repository.CRUD
{
    public interface ICRUDRepository<TEntity> where TEntity : EntityClass
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetById(int id);
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
