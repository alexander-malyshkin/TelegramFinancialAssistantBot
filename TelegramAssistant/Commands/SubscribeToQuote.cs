using StockSharp.BusinessEntities;
using TelegramAssistant.Events;

namespace TelegramAssistant.Commands
{
    internal class SubscribeToQuote<S> : CommandBase<S, Security>  where S: IQuoteReceiver
    {
        
        public SubscribeToQuote(S sender, Security requestSecurity ):base(sender, requestSecurity)
        {}

       


        
    }
}