using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;
using TelegramAssistant.Types;

namespace TelegramAssistant.NotificationSubscribers
{
    class NotificationSubscriber : INotificationSubscriber
    {
        private static ConcurrentQueue<NotificationTask> _subscriptionsQueue 
            = new ConcurrentQueue<NotificationTask>();

        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        public NotificationSubscriber(IExchangeRatesProvider exchangeRatesProvider)
        {
            _exchangeRatesProvider = exchangeRatesProvider;
        }

        public async Task Subscribe(string asset, Func<decimal, bool> predicate)
        {
            var assetValue = await _exchangeRatesProvider.GetAssetValue(asset);
            if (predicate(assetValue))
                throw new NotSupportedException("Условие по данному активу уже выполняется");

            _subscriptionsQueue.Enqueue(new NotificationTask
            {
                Asset = asset,
                Predicate = predicate
            });
        }

        public async Task<bool> CanSubscribe(string asset, Func<decimal, bool> predicate)
        {
            var assetValue = await _exchangeRatesProvider.GetAssetValue(asset);
            return !predicate(assetValue);
        }
    }
}
