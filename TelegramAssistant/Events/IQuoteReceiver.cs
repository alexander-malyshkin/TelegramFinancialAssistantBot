using System.Threading.Tasks;
using StockSharp.BusinessEntities;

namespace TelegramAssistant.Events
{
    public interface IQuoteReceiver
    {
         void HandleQuote(MarketDepth marketDepth);
    }
}