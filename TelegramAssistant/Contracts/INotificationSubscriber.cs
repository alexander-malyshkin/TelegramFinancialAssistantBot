using System;
using System.Threading.Tasks;

namespace TelegramAssistant.Contracts
{
    public interface INotificationSubscriber
    {
        Task Subscribe(string asset, Func<decimal, bool> predicate);
        Task<bool> CanSubscribe(string asset, Func<decimal, bool> predicate);
    }
}