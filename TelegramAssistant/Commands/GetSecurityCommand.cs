using StockSharp.BusinessEntities;

namespace TelegramAssistant.Commands
{
    public class GetSecurityCommand<S> : CommandBase<S, string> 
    {
      public GetSecurityCommand(S sender, string ticker) : base(sender, ticker)
            { }





        
    }
}