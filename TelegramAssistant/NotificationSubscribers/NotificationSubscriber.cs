using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;

namespace TelegramAssistant.NotificationSubscribers
{
    class NotificationSubscriber : INotificationSubscriber
    {
        internal static Collection<NotificationTask> Subscriptions { get; } 
            = new Collection<NotificationTask>();

        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        public NotificationSubscriber(IExchangeRatesProvider exchangeRatesProvider)
        {
            _exchangeRatesProvider = exchangeRatesProvider;
        }

        public async Task Subscribe(string asset, long chatId, Func<decimal, bool> predicate)
        {
            var assetValue = await _exchangeRatesProvider.GetAssetValue(asset);
            if (predicate(assetValue))
                throw new NotSupportedException("Условие по данному активу уже выполняется");

            if(await AlreadySubscribed(asset, chatId, predicate))
                throw new NotSupportedException("Вы уже подписаны на данное событие");

            Subscriptions.Add(new NotificationTask
            {
                Asset = asset,
                ChatId = chatId,
                Predicate = predicate
            });
        }

        public async Task<bool> ConditionAlreadyApplies(string asset, long chatId, Func<decimal, bool> predicate)
        {
            var assetValue = await _exchangeRatesProvider.GetAssetValue(asset);
            return predicate(assetValue);
        }

        public async Task<bool> AlreadySubscribed(string asset, long chatId, Func<decimal, bool> predicate)
        {
            return Subscriptions.Any(s => s.Asset.Equals(asset, StringComparison.InvariantCultureIgnoreCase)
                                                && s.ChatId == chatId && s.Predicate == predicate);
        }
    }
}
