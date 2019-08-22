using StockSharp.BusinessEntities;
using TelegramAssistant.Events;

namespace TelegramAssistant.Commands
{
    public class UnsubscribeToQuote<S> : CommandBase<S, Security> where S : IQuoteReceiver
    {
        public UnsubscribeToQuote(S sender, Security requestSecurity) : base(sender, requestSecurity)
        { }

    }
}