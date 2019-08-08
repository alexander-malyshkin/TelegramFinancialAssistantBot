using System.Threading.Tasks;

namespace TelegramAssistant.Contracts
{
    public interface ICommand<TResponse> where TResponse : class
    {
        Task<bool> Validate();
        Task<TResponse> Process();
    }
}