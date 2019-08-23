using System.Threading.Tasks;
using StockSharp.BusinessEntities;
using TelegramAssistant.Events;
using TelegramAssistant.Types.Responses;

namespace TelegramAssistant.Services.QuikTerminalService
{
    public interface IQuikTerminalService
    {
        Task<IResponse> SubscribeToQuote(Security security, IQuoteReceiver quoteReceiver);
        Task<IResponse>  GetSecurity(string ticker);
    }
}