using System;
using System.Threading.Tasks;
using TelegramAssistant.Types.Requests;

namespace TelegramAssistant.Contracts
{
    public interface INotificationSubscriber
    {
        Task Subscribe(Action<RequestBase> action);
    }
}