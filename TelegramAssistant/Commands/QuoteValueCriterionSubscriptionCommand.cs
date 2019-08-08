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

        public QuoteValueCriterionSubscriptionCommand(QuoteValueCriterionSubscriptionRequest request)
        {
            _request = request;
        }

        public async Task<bool> Validate()
        {

            var supportedAssets = Enum.GetNames(typeof(Asset));
            if (supportedAssets.All(supportedAsset => !_request.Asset.StartsWith(supportedAsset)))
            {
                return false;
            }

            return true;
        }

        public async Task<QuoteValueCriterionSubscriptionResponse> Process()
        {
            throw new NotImplementedException();
        }
    }
}