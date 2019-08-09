using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using TelegramAssistant.Contracts;

namespace TelegramAssistant.Services
{
    class QueuedHostedService : BackgroundService
    {

        public IBackgroundTaskQueue TaskQueue { get; }

        public QueuedHostedService(IBackgroundTaskQueue taskQueue)
        {
            TaskQueue = taskQueue;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var workItem = await TaskQueue.DequeueAsync(cancellationToken);

                await workItem(cancellationToken);
            }
        }
    }
}
