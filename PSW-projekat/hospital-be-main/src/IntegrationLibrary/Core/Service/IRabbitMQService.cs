using IntegrationLibrary.Core.Model;
using System.Collections.Generic;

namespace IntegrationLibrary.Core.Service
{
    public interface IRabbitMQService
    {
        void Send();
        void SendScheduledOrder(ScheduledOrder scheduledOrder);
        List<News> Recive(List<BloodBank> bloodBanks);
        public List<FilledOrder> ReciveSheduledOrders(List<BloodBank> bloodBanks);
    }
}
