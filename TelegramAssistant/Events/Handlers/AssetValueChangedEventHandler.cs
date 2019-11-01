using System.Threading.Tasks;
using Telegram.Bot;
using TelegramAssistant.Contracts;

namespace TelegramAssistant.Events.Handlers
{
    class AssetValueChangedEventHandler : IAssetValueChangedEventHandler
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private const string NtFormat = MessageTexts.AssetCurrentValue;

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
