using TelegramAssistant.Contracts;

namespace TelegramAssistant.Commands
{
    public class CommandBase<S, R> : ICommand<S, R> 
    {
        public CommandBase(S sender, R request)
        {
            Sender = sender;
            Request = request;
        }

        public static ICommand<S, R> Command(S sender, R request)
        {
            return new CommandBase<S, R>(sender, request);
        }

        public S Sender { get; }
        public R Request { get;  }
    }
}