using System;
using System.Threading;
using System.Threading.Tasks;
using IntegrationLibrary.Core.Service.Reports;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegrationLibrary.Core.Service.HostedServices
{
    public class BloodReportHostedService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly int ReportIntervalInHours = 24;       //svaki dan
        private Timer timer;

        public BloodReportHostedService(IServiceScopeFactory scopeFactory)
        {
            this.scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            // Invoke the DoWork method every 5 seconds. 
            timer = new Timer(callback: async o => await DoWork(o),
            state: null, dueTime: TimeSpan.FromSeconds(0),
            period: TimeSpan.FromHours(ReportIntervalInHours));
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            } // Change the start time to infinite, thereby stop the timer.
            timer?.Change(Timeout.Infinite, 0); return Task.CompletedTask;
        }

        private async Task<bool> DoWork(Object o)
        {
            
            using (var scope = scopeFactory.CreateScope())
            {
                var reportSendingService = scope.ServiceProvider.GetService<IReportSendingService>();
                bool isSuccess = false;
                try
                {
                    if (reportSendingService.ReportShouldBeSent())
                        isSuccess = await reportSendingService.GeneratePDFs();
                    if (isSuccess)
                        reportSendingService.ChangeReportDeliveryDate();
                    
                    return isSuccess;

                }
                catch (Exception e)
                {
                    reportSendingService.DeleteMadeFiles();
                    return false;
                }
            }
        }
    }
}
