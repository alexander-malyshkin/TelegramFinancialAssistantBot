using System;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;
using TelegramAssistant.Types;

namespace TelegramAssistant.Mocks
{
    internal class SberbankExchangeRatesProvider : IExchangeRatesProvider
    {
        private const int _taskDelay = 700;
        private const int _maxAssetValue = 500;

        public async Task<decimal> GetAssetValue(Asset asset)
        {
            await Task.Delay(_taskDelay);
            var rnd = new Random();
            return rnd.Next(_maxAssetValue);
        }
    }
}
