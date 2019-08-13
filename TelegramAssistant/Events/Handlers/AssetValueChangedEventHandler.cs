using System.Threading.Tasks;
using Telegram.Bot;
using TelegramAssistant.Contracts;

namespace TelegramAssistant.Events.Handlers
{
    class AssetValueChangedEventHandler : IAssetValueChangedEventHandler
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private const string NtFormat = "Актив {0} теперь имеет цену {1}";

        public AssetValueChangedEventHandler(App app)
        {
            _telegramBotClient = app.Bot;
        }

        public async Task Handle(object sender, AssetValueChangedEventArgs args)
        {
            await _telegramBotClient.SendTextMessageAsync(args.ChatId,
                string.Format(NtFormat, args.Asset, args.Value));
        }
    }
}
