using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramAssistant.Commands;
using TelegramAssistant.Settings;
using TelegramAssistant.Types;
using TelegramAssistant.Types.Requests;

namespace TelegramAssistant
{
    public class App
    {
        private AppSettings AppSettings { get; }
        private SensitiveSettings SensitiveConfiguration { get; }
        private static TelegramBotClient Bot { get; set; }

        private IList<string> _supportedAssets = Enum.GetNames(typeof(Asset));

        private static string _quoteUsageFormat;
        private const int _chatInteractionDelay = 500;

        public App()
        {
            var settingsDir = Path.Combine(Directory.GetCurrentDirectory(), "Settings");
            var configLocation = Path.Combine(settingsDir, "AppSettings.json");
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(configLocation);

            IConfiguration configuration = configurationBuilder.Build();
            AppSettings = new SettingsBuilder<AppSettings>(configuration).Build();

            var sensitiveConfigFile = AppSettings.SensitiveConfigFile;
            var sensConfBuilder = new ConfigurationBuilder()
                .AddJsonFile(sensitiveConfigFile);
            IConfiguration sensConfiguration = sensConfBuilder.Build();
            SensitiveConfiguration = new SettingsBuilder<SensitiveSettings>(sensConfiguration).Build();

            Bot = SensitiveConfiguration.ProxyEnabled ? 
                new TelegramBotClient(SensitiveConfiguration.BotApiKey, new WebProxy(SensitiveConfiguration.Proxy))
                : new TelegramBotClient(SensitiveConfiguration.BotApiKey);
            ConfigureBot();

            SetupUsages();
        }

        private void SetupUsages()
        {
            _quoteUsageFormat = @"/quote <название_актива> <оператор> <значение_актива> then <действие>. \n"
                                + $"Поддерживаемые активы {string.Join(',', _supportedAssets)}"
                                + $"Операторы: >, >=, <, <= \n"
                                + $"Действия: signal";
        }

        private void ConfigureBot()
        {
            Bot.OnMessage += BotOnMessage;
            Bot.StartReceiving(Array.Empty<UpdateType>());
        }

        public void StopReceiving()
        {
            Bot.StopReceiving();
        }

        private static async void BotOnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != MessageType.Text) return;

            var cmdArgs = message.Text.Split(' ');
            switch (cmdArgs.First())
            {
                case QuoteValueCriterionSubscriptionRequest.CommandShortcutStatic: // e.g. - /quote sber >250 then signal
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(_chatInteractionDelay);
                    try
                    {
                        var quoteRequest = new QuoteValueCriterionSubscriptionRequest(cmdArgs);
                        var quoteCmd = new QuoteValueCriterionSubscriptionCommand(quoteRequest);
                        var response = await quoteCmd.Process();
                        await Bot.SendTextMessageAsync(message.Chat.Id, response.ResultMessage);
                    }
                    catch (Exception)
                    {
                        await Bot.SendTextMessageAsync(message.Chat.Id, _quoteUsageFormat);
                    }

                    break;
                default:
                    await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                    await Task.Delay(_chatInteractionDelay);
                    await Bot.SendTextMessageAsync(message.Chat.Id, _quoteUsageFormat);
                    break;
            }
        }
    }
}
