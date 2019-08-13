using System.Threading.Tasks;
using TelegramAssistant.Events;

namespace TelegramAssistant.Contracts
{
    public interface IAssetValueChangedEventHandler
    {
        Task Handle(object sender, AssetValueChangedEventArgs args);
    }
}