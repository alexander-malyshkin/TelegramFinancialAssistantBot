using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;
using TelegramAssistant.Types.Requests;

namespace TelegramAssistant.NotificationSubscribers
{
    class NotificationSubscriber : INotificationSubscriber
    {
        private static ConcurrentQueue<Action<RequestBase>> _subscriptionsQueue 
            = new ConcurrentQueue<Action<RequestBase>>();

        public async Task Subscribe(Action<RequestBase> action)
        {
            _subscriptionsQueue.Enqueue(action);
        }
    }
}
