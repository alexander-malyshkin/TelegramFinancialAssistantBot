using System.Threading.Tasks;
using TelegramAssistant.Types;

namespace TelegramAssistant.Contracts
{
    public interface IExchangeRatesProvider
    {
        Task<decimal> GetAssetValue(Asset asset);
    }
}