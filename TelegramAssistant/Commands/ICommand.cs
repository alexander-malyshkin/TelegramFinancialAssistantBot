namespace TelegramAssistant.Contracts
{
    public interface ICommand<S, R>
    {


        S Sender { get; }
        R Request { get; }
    
        
    }
}