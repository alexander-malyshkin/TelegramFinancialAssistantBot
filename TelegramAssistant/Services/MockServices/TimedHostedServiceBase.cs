using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TelegramAssistant.Services
{
    public abstract class TimedHostedServiceBase : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly int _intervalMs;
        private readonly int _dueTimeSpanSeconds;

        protected TimedHostedServiceBase(int intervalMs, int dueTimeSpanSeconds)
        {
            _intervalMs = intervalMs;
            _dueTimeSpanSeconds = dueTimeSpanSeconds;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _timer = new Timer(DoWork, null, TimeSpan.FromSeconds(_dueTimeSpanSeconds),
                TimeSpan.FromMilliseconds(_intervalMs));

            return Task.CompletedTask;
        }

        protected abstract void DoWork(object state);

        

        public Task StopAsync(CancellationToken cancellationToken)
        {
            //_logger.LogInformation("Timed Background Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
