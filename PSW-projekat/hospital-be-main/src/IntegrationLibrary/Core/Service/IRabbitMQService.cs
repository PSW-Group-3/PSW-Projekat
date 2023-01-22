using IntegrationLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service
{
    public interface IRabbitMQService
    {
        void Send();
        void SendScheduledOrder(ScheduledOrder scheduledOrder);
        List<News> Recive(List<BloodBank> bloodBanks);
        public List<FilledOrder> ReciveSheduledOrders(List<BloodBank> bloodBanks);

        void ResetFilledOrders();
    }
}
