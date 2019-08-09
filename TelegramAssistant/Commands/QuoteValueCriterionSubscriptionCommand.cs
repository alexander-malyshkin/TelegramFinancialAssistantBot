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
        private readonly INotificationSubscriber _notificationSubscriber;
        private readonly IExchangeRatesProvider _exchangeRatesProvider;

        private string _asset;

        public QuoteValueCriterionSubscriptionCommand(QuoteValueCriterionSubscriptionRequest request, 
            INotificationSubscriber notificationSubscriber, IExchangeRatesProvider exchangeRatesProvider)
        {
            _request = request ?? throw new ArgumentException($"Не указан запрос {nameof(QuoteValueCriterionSubscriptionRequest)}");
            _notificationSubscriber = notificationSubscriber ?? throw new ArgumentException($"Не указан {nameof(INotificationSubscriber)}");
            _exchangeRatesProvider = exchangeRatesProvider;
        }

        public async Task<bool> Validate()
        {

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
            try
            {
                var canSubscribe = await _notificationSubscriber.CanSubscribe(_asset, _request.Predicate);
                if(!canSubscribe)
                {
                    return new QuoteValueCriterionSubscriptionResponse
                    {
                        Success = true,
                        ResultMessage = "Указанный актив уже удовлетворяет условию"
                    };
                }
            }
            catch (Exception e)
            {
                return new QuoteValueCriterionSubscriptionResponse
                {
                    Success = false,
                    ResultMessage = "Не получилось проверить выполнение условия по указанному активу"
                };
            }

            try
            {
                await _notificationSubscriber.Subscribe(_asset, _request.Predicate);
                return new QuoteValueCriterionSubscriptionResponse
                {
                    Success = true,
                    ResultMessage = "Вы успешно подписались на событие"
                };
            }
            catch (Exception e)
            {
                return new QuoteValueCriterionSubscriptionResponse
                {
                    Success = false,
                    ResultMessage = $"Не удалось подписаться на событие"
                };
            }
        }
    }
}