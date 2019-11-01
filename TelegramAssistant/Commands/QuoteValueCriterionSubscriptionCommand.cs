using System;
using System.Linq;
using System.Threading.Tasks;
using TelegramAssistant.Contracts;
using TelegramAssistant.Types;
using TelegramAssistant.Types.Requests;
using TelegramAssistant.Types.Responses;

namespace TelegramAssistant.Commands
{
    internal class QuoteValueCriterionSubscriptionCommand : ICommand<QuoteValueCriterionSubscriptionResponse>
    {
        private readonly QuoteValueCriterionSubscriptionRequest _request;
        private readonly ISubscriptionsManager _subscriptionsManager;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        private string _asset;

        public QuoteValueCriterionSubscriptionCommand(QuoteValueCriterionSubscriptionRequest request, 
            ISubscriptionsManager subscriptionsManager, IExchangeRatesProvider exchangeRatesProvider)
        {
            _request = request ?? throw new ArgumentException($"{nameof(QuoteValueCriterionSubscriptionRequest)} {MessageTexts.NotSpecified}");
            _subscriptionsManager = subscriptionsManager ?? throw new ArgumentException($"{nameof(ISubscriptionsManager)} {MessageTexts.NotSpecified}");
            _exchangeRatesProvider = exchangeRatesProvider ?? throw new ArgumentException($"{nameof(IExchangeRatesProvider)} {MessageTexts.NotSpecified}");
        }

        public async Task<bool> Validate()
        {
            if (_request.ChatId == 0) return false;
            var supportedAssets = await _exchangeRatesProvider.GetAssets();
            if (supportedAssets.All(supportedAsset => !_request.Asset.StartsWith(supportedAsset)))
            {
                return false;
            }

            _asset = _request.Asset.Split(_request.Operator).First();
            return true;
        }

        public async Task<QuoteValueCriterionSubscriptionResponse> Process()
        {
            decimal currentValue;
            try
            {
                currentValue = await _exchangeRatesProvider.GetAssetValue(_asset);
                var conditionAlreadyApplies = _request.Predicate(currentValue);

                if (conditionAlreadyApplies)
                {
                    return new QuoteValueCriterionSubscriptionResponse
                    {
                        Success = false,
                        ResultMessage = string.Format(MessageTexts.ConditionAlreadySatisfied, currentValue)
                    };
                }

                var alreadySubscribed = await _subscriptionsManager.AlreadySubscribed(_asset, _request.ChatId, _request.Predicate);
                if (alreadySubscribed)
                {
                    return new QuoteValueCriterionSubscriptionResponse
                    {
                        Success = true,
                        ResultMessage = MessageTexts.AlreadySubscribed
                    };
                }

                
            }
            catch (Exception)
            {
                return new QuoteValueCriterionSubscriptionResponse
                {
                    Success = false,
                    ResultMessage = MessageTexts.ConditionCheckFailed
                };
            }

            try
            {
                await _subscriptionsManager.Subscribe(_asset, _request.ChatId, _request.Predicate);
                return new QuoteValueCriterionSubscriptionResponse
                {
                    Success = true,
                    ResultMessage = string.Format(MessageTexts.SubscribedSuccessfully, currentValue),
                    CurrentPrice = currentValue
                };
            }
            catch (Exception e)
            {
                return new QuoteValueCriterionSubscriptionResponse
                {
                    Success = false,
                    ResultMessage = $"{MessageTexts.SubscriptionFailed}. {e.Message}"
                };
            }
        }
    }
}