using IntegrationAPI.DTO;
using Microsoft.AspNetCore.Mvc;

namespace IntegrationAPI.Controllers.Interfaces
{
    public interface ICRUDController<TEntity> where TEntity : BaseModelDTO
    {
        ActionResult GetAll();
        ActionResult GetById(int id);
        ActionResult Create(TEntity entity);
        ActionResult Update(int id, TEntity entity);
        ActionResult Delete(int id);
    }
}
