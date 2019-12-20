using EasywebshopProductFeedAdapter.Exceptions;
using EasywebshopProductFeedAdapter.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasywebshopProductFeedAdapter.Services
{
    //Modified from https://leeconlin.co.uk/blog/recurring-tasks-in-net-core-20-without-a-scheduling-library
    public class FeedUpdateService : IHostedService, IDisposable
    {
        private Task _executingTask;
        private readonly CancellationTokenSource _stoppingCts = new CancellationTokenSource();
        private readonly IFeedGeneratorService _feedGeneratorService;
        private readonly IFeedWriterService _feedWriterService;
        private readonly long _interval;


        public FeedUpdateService(IConfiguration config, IFeedGeneratorService feedGeneratorService, IFeedWriterService feedWriterService)
        {
            _feedGeneratorService = feedGeneratorService;
            _feedWriterService = feedWriterService;
            _interval = config.GetValue<long>("Update_Interval");
        }

        protected async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // This will cause the loop to stop if the service is stopped
            while (!stoppingToken.IsCancellationRequested)
            {
                //Get the feed from EasyWebshop and make it Google Merchant compatible
                try
                {
                    var feed = await _feedGeneratorService.GetProductFeedAsync();
                    //Write it to a file (rss.xml)
                    _feedWriterService.Write(feed);
                }
                catch (HttpException e)
                {
                    Debug.WriteLine($"Feed generator returned error {e.StatusCode}, not updating feed");
                }

                // Wait configured time in minutes before running again.
                await Task.Delay(TimeSpan.FromMinutes(_interval), stoppingToken);
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Store the task we're executing
            _executingTask = ExecuteAsync(_stoppingCts.Token);

            // If the task is completed then return it,
            // this will bubble cancellation and failure to the caller
            if (_executingTask.IsCompleted)
            {
                return _executingTask;
            }

            // Otherwise it's running
            return Task.CompletedTask;
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            // Stop called without start
            if (_executingTask == null)
            {
                return;
            }

            try
            {
                // Signal cancellation to the executing method
                _stoppingCts.Cancel();
            }
            finally
            {
                // Wait until the task completes or the stop token triggers
                await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                    cancellationToken));
            }
        }

        public void Dispose()
        {
            _stoppingCts.Cancel();
        }
    }
}
