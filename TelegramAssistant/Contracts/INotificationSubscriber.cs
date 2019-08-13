using System;
using System.Threading.Tasks;

namespace TelegramAssistant.Contracts
{
    public interface INotificationSubscriber
    {
        Task Subscribe(string asset, long chatId, Func<decimal, bool> predicate);
        Task Unsubscribe(string asset, long chatId, Func<decimal, bool> predicate);
        Task<bool> ConditionAlreadyApplies(string asset, long chatId, Func<decimal, bool> predicate);
        Task<bool> AlreadySubscribed(string asset, long chatId, Func<decimal, bool> predicate);
        
    }
}