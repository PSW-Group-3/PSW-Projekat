using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.TenderStatistic
{
    public interface ITenderStatisticsService
    {
        void CreateStatisticsBloodType(DateTime start, DateTime end);
        void CreateStatisticsBloodBank(DateTime start, DateTime end);
    }
}
