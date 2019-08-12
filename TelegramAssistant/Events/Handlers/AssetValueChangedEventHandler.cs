using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace TelegramAssistant.Events.Handlers
{
    class AssetValueChangedEventHandler
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private const string NtFormat = "Актив {0} теперь имеет цену {1}";

        public AssetValueChangedEventHandler(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task Handle(object sender, AssetValueChangedEventArgs args)
        {
            await _telegramBotClient.SendTextMessageAsync(args.ChatId,
                string.Format(NtFormat, args.Asset, args.Value));
        }
    }
}
