using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;
using TelegramAssistant.Types;

namespace TelegramAssistant.ExchangeRateProviders
{
    internal class MockExchangeRatesProvider : IExchangeRatesProvider
    {
        private const int _taskDelay = 700;
        private const int _maxAssetValue = 500;
        private readonly ICollection<string> _assets;

        public MockExchangeRatesProvider()
        {
            _assets = new Collection<string>
            {
                "Sberbank",
                "Rosneft",
                "Gazprom"
            };
        }

        public async Task<decimal> GetAssetValue(string asset)
        {
            return _assets.Any(a =>
                a.Equals(asset, StringComparison.InvariantCultureIgnoreCase))
                ? GetRandomValue()
                : throw new KeyNotFoundException();
        }

        public async Task<ICollection<string>> GetAssets()
        {
            return _assets;
        }

        private decimal GetRandomValue()
        {
            var rnd = new Random();
            return rnd.Next(_maxAssetValue);
        }
    }
}
