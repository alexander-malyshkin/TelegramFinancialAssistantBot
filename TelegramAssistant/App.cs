using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TelegramAssistant.Commands;
using TelegramAssistant.Contracts;
using TelegramAssistant.Settings;
using TelegramAssistant.Types.Requests;

namespace TelegramAssistant
{
    public class App
    {
        internal AppSettings AppSettings { get; }
        private SensitiveSettings SensitiveConfiguration { get; }
        public TelegramBotClient Bot { get; }
        private IServiceContainer ServiceContainer { get; }

        private ICollection<string> _supportedAssets;

        private static string _quoteUsageFormat;
        private string _internalErrorMsg = "Что-то пошло не так";
        private string _notImplementedExcMsg = "Данный функционал не поддерживается, свяжитесь с разработчиком";
        private const int _chatInteractionDelay = 500;

        public App(IServiceContainer sc)
        {
            ServiceContainer = sc;

            var settingsDir = Path.Combine(Directory.GetCurrentDirectory(), "Settings");
            var configLocation = Path.Combine(settingsDir, "AppSettings.json");
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(configLocation);

            IConfiguration configuration = configurationBuilder.Build();
            AppSettings = new SettingsBuilder<AppSettings>(configuration).Build();

            if (AppSettings.NotificationSettings == null)
                throw new ArgumentNullException(nameof(NotificationSettings));

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
            var ratesProvider = (IExchangeRatesProvider) ServiceContainer.GetService(typeof(IExchangeRatesProvider));
            _supportedAssets = (ratesProvider.GetAssets()).GetAwaiter().GetResult();
            _quoteUsageFormat = @"/quote <название_актива> <оператор> <значение_актива> then <действие>. "
                                + $"Поддерживаемые активы {string.Join(',', _supportedAssets)}. "
                                + $"Операторы: >, >=, <, <=. "
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

        private async void BotOnMessage(object sender, MessageEventArgs e)
        {
            var message = e.Message;

            if (message == null || message.Type != MessageType.Text) return;

            var cmdArgs = message.Text.Split(' ');

            try
            {
                switch (cmdArgs.First())
                {
                    case QuoteValueCriterionSubscriptionRequest.CommandShortcutStatic
                        : // e.g. - /quote Sberbank>250 then signal
                        await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                        await Task.Delay(_chatInteractionDelay);
                        var quoteRequest = new QuoteValueCriterionSubscriptionRequest(cmdArgs, message.Chat.Id);
                        var quoteCmd = new QuoteValueCriterionSubscriptionCommand(quoteRequest, 
                            (INotificationSubscriber) ServiceContainer.GetService(typeof(INotificationSubscriber)),
                            (IExchangeRatesProvider) ServiceContainer.GetService(typeof(IExchangeRatesProvider)));
                        var requestValid = await quoteCmd.Validate();

                        if (!requestValid)
                        {
                            await Bot.SendTextMessageAsync(message.Chat.Id, _quoteUsageFormat);
                            break;
                        }

                        var response = await quoteCmd.Process();
                        await Bot.SendTextMessageAsync(message.Chat.Id, response.ResultMessage);
                        break;

                    default:
                        await Bot.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);
                        await Task.Delay(_chatInteractionDelay);
                        await Bot.SendTextMessageAsync(message.Chat.Id, _quoteUsageFormat);
                        break;
                }
            }
            catch (ArgumentException)
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, _quoteUsageFormat);
            }
            catch (NotImplementedException)
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, _notImplementedExcMsg);
            }
            catch (Exception)
            {
                await Bot.SendTextMessageAsync(message.Chat.Id, _internalErrorMsg);
            }
        }
    }
}
