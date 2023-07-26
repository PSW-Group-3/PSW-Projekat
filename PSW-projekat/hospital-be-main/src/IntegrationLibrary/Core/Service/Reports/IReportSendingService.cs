using System;
using System.Threading.Tasks;

namespace IntegrationLibrary.Core.Service.Reports
{
    public interface IReportSendingService
    {
        bool ReportShouldBeSent();
        int GetDaysSpanTillToday(DateTime date);
        Task<bool> GeneratePDFs();
        void ChangeReportDeliveryDate();
        void DeleteMadeFiles();

    }
}
