using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace IntegrationLibrary.Core.Service
{
    public class BloodReportHostedService : IHostedService
    {
        private readonly int ReportIntervalInSecs = 60;
        private Timer timer;
        public Task StartAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            // Invoke the DoWork method every 5 seconds. 
            timer = new Timer(callback: async o => await DoWork(o),
            state: null, dueTime: TimeSpan.FromSeconds(0),
            period: TimeSpan.FromSeconds(ReportIntervalInSecs));
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

        private async Task DoWork(Object o)
        {
            
        }
    }
}
