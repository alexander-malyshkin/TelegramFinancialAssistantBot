using System;
using System.Threading.Tasks;
using TelegramAssistant.Types.Responses;


namespace TelegramAssistant.Contracts
{
    public interface ISubscriptionsManager
    {
        Task<IResponse> ConsumeCommand<S, Req>(ICommand<S, Req> command);
    }
}