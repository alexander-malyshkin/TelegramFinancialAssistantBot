using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace TelegramAssistant.Services
{
    internal class CommandProcessingService : IHostedService, IDisposable
    {
        private readonly App _app;

        public CommandProcessingService(App app)
        {
            _app = app;
        }

        public void Dispose()
        {
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _app.StartReceiving();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _app.StopReceiving();
            return Task.CompletedTask;
        }
    }
}
