using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TelegramAssistant.Contracts;
using TelegramAssistant.Events;
using TelegramAssistant.NotificationSubscribers;

namespace TelegramAssistant.Services
{
    class TimedHostedNotificationService : TimedHostedServiceBase
    {
        private readonly IExchangeRatesProvider _exchangeRatesProvider;
        internal event EventHandler<AssetValueChangedEventArgs> AssetValueChangedEvent;

        public TimedHostedNotificationService(
            int intervalMs, 
            int dueTimeSpanSeconds, 
            IExchangeRatesProvider exchangeRatesProvider) 
            : base(intervalMs, 
                dueTimeSpanSeconds)
        {
            _exchangeRatesProvider = exchangeRatesProvider;
        }

        protected override void DoWork(object state)
        {
            SimulateAssetValueChanges();
        }

        private void SimulateAssetValueChanges()
        {
            var notificationSubscriptions = NotificationSubscriber.Subscriptions;

            var assets = _exchangeRatesProvider.GetAssets().GetAwaiter().GetResult().ToArray();
            var rnd = new Random();
            var changedAssetsAmount = rnd.Next(assets.Length);
            for (var i = 0; i < changedAssetsAmount; i++)
            {
                var randomAssetInd = rnd.Next(assets.Length);
                var randomAsset = assets[randomAssetInd];
                var assetValue = _exchangeRatesProvider.GetAssetValue(randomAsset).GetAwaiter().GetResult();

                var matchedConditions = notificationSubscriptions.Where(ns =>
                    ns.Predicate(assetValue) &&
                    ns.Asset.Equals(randomAsset, StringComparison.InvariantCultureIgnoreCase));
                foreach (var matchedCondition in matchedConditions)
                {
                    AssetValueChangedEvent?.Invoke(this, new AssetValueChangedEventArgs
                    {
                        Asset = matchedCondition.Asset,
                        ChatId = matchedCondition.ChatId,
                        Value = assetValue
                    });
                }
            }
        }
    }
}
