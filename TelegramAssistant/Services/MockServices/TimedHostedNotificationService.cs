using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;
using TelegramAssistant.Events;
using TelegramAssistant.NotificationSubscribers;
using TelegramAssistant.Settings;

namespace TelegramAssistant.Services
{
    //class TimedHostedNotificationService : TimedHostedServiceBase
    //{
    //    private readonly IExchangeRatesProvider _exchangeRatesProvider;
    //    private readonly ISubscriptionsManager _subscriptionsManager;
    //    internal event EventHandler<AssetValueChangedEventArgs> AssetValueChangedEvent;

    //    public TimedHostedNotificationService(
    //        NotificationSettings notificationSettings, 
    //        IExchangeRatesProvider exchangeRatesProvider,
    //        IAssetValueChangedEventHandler assetValueChangedEventHandler, 
    //        ISubscriptionsManager subscriptionsManager) 
    //        : base(notificationSettings.IntervalMilliseconds, 
    //            notificationSettings.DueTimeSpanSeconds)
    //    {
    //        _exchangeRatesProvider = exchangeRatesProvider;
    //        _subscriptionsManager = subscriptionsManager;
    //        AssetValueChangedEvent += async (sender, args) => await assetValueChangedEventHandler.Handle(sender, args);
    //    }

    //    protected override void DoWork(object state)
    //    {
    //        SimulateAssetValueChanges();
    //    }

    //    private void SimulateAssetValueChanges()
    //    {
    //        var notificationSubscriptions = SubscriptionsManager.Subscriptions;

    //        var assets = _exchangeRatesProvider.GetAssets().GetAwaiter().GetResult().ToArray();
    //        var rnd = new Random();
    //        var changedAssetsAmount = rnd.Next(assets.Length);
    //        for (var i = 0; i < changedAssetsAmount; i++)
    //        {
    //            var randomAssetInd = rnd.Next(assets.Length);
    //            var randomAsset = assets[randomAssetInd];
    //            var assetValue = _exchangeRatesProvider.GetAssetValue(randomAsset).GetAwaiter().GetResult();

    //            var matchedConditions = notificationSubscriptions.Where(ns =>
    //                ns.Predicate(assetValue) &&
    //                ns.Asset.Equals(randomAsset, StringComparison.InvariantCultureIgnoreCase))
    //                .ToArray();

    //            foreach (var matchedCondition in matchedConditions)
    //            {
    //                AssetValueChangedEvent?.Invoke(this, new AssetValueChangedEventArgs
    //                {
    //                    Asset = matchedCondition.Asset,
    //                    ChatId = matchedCondition.ChatId,
    //                    Value = assetValue
    //                });
    //            }

    //            foreach (var cond in matchedConditions)
    //            {
    //                _subscriptionsManager.Unsubscribe(cond.Asset, cond.ChatId, cond.Predicate);
    //            }
    //        }
    //    }
    //}
}
