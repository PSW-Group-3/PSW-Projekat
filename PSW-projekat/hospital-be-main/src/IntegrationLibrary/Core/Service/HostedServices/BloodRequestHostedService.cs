using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IntegrationLibrary.Core.Model;
using IntegrationLibrary.Core.Service.BloodRequests;
using IntegrationLibrary.Core.SFTPConnection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegrationLibrary.Core.Service.HostedServices
{
    public class BloodRequestHostedService : IHostedService
    {
        private readonly IServiceScopeFactory scopeFactory;
        private readonly int ReportIntervalInHours = 24;       //svaki dan
        private Timer timer;

        public BloodRequestHostedService(IServiceScopeFactory scopeFactory)
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
                var bloodRequestService = scope.ServiceProvider.GetService<IBloodRequestService>();
                try
                {
                    if (bloodRequestService.RequestShouldBeSent())
                    {
                        foreach(BloodRequest request in bloodRequestService.GetRequestsThatShouldBeSent())
                        {
                            bloodRequestService.GetBloodFromBloodBank(request);
                        }
                    }
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            }
        }
    }
}
