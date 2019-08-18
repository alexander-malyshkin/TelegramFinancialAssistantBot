using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;

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

        public async Task<bool> ConditionAlreadyApplies(string asset, long chatId, Func<decimal, bool> predicate)
        {
            var foundAsset = _assets.FirstOrDefault(a => a.Equals(asset, StringComparison.InvariantCultureIgnoreCase));
            if(foundAsset == null)
                throw new ArgumentException($"Актив {asset} не поддерживается");

            var assetValue = await GetAssetValue(foundAsset);
            return predicate(assetValue);
        }

        private decimal GetRandomValue()
        {
            var rnd = new Random();
            return rnd.Next(_maxAssetValue);
            //return 130m;
        }
    }
}
