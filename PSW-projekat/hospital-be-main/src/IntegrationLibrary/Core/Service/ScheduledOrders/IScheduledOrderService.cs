using IntegrationLibrary.Core.Service.CRUD;

namespace IntegrationLibrary.Core.Service.ScheduledOrders
{
    public interface IScheduledOrderService : ICRUDService<Model.ScheduledOrder>
    {
        public void ReadOrederedBlood();
    }
}
