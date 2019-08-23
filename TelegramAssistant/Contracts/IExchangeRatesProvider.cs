using System.Collections.Generic;
using System.Threading.Tasks;

namespace TelegramAssistant.Contracts
{
    public interface IExchangeRatesProvider
    {
        Task<decimal> GetAssetValue(string asset);
        Task<ICollection<string>> GetAssets();
    }
}