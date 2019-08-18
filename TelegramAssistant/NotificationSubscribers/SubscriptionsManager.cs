using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;

namespace TelegramAssistant.NotificationSubscribers
{
    class SubscriptionsManager : ISubscriptionsManager
    {
        public static Collection<NotificationTask> Subscriptions { get; } 
            = new Collection<NotificationTask>();

        public async Task Subscribe(string asset, long chatId, Func<decimal, bool> predicate)
        {
            if(await AlreadySubscribed(asset, chatId, predicate))
                throw new NotSupportedException("Вы уже подписаны на данное событие");

            Subscriptions.Add(new NotificationTask
            {
                Asset = asset,
                ChatId = chatId,
                Predicate = predicate
            });
        }

        public async Task Unsubscribe(string asset, long chatId, Func<decimal, bool> predicate)
        {
            var ntTask = Subscriptions.FirstOrDefault(s => 
                s.Asset.Equals(asset, StringComparison.InvariantCultureIgnoreCase) 
                    && s.ChatId == chatId 
                    && s.Predicate == predicate);

            if (ntTask != null)
            {
                Subscriptions.Remove(ntTask);
            }
        }

        public async Task<bool> AlreadySubscribed(string asset, long chatId, Func<decimal, bool> predicate)
        {
            return Subscriptions.Any(s => s.Asset.Equals(asset, StringComparison.InvariantCultureIgnoreCase)
                                                && s.ChatId == chatId && s.Predicate == predicate);
        }

    }
}
