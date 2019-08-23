using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QuickTerminalConsole;
using StockSharp.BusinessEntities;
using Telegram.Bot;
using TelegramAssistant.Contracts;
using TelegramAssistant.ExchangeRateProviders;
using TelegramAssistant.NotificationSubscribers;
using TelegramAssistant.Services;
using TelegramAssistant.Services.MockServices;
using TelegramAssistant.Services.QuikTerminalService;
using TelegramAssistant.Settings;

namespace TelegramAssistant
{
    public static class Program
    {
        private static BotServiceSettings _botServiceSettings = SettingsHelper.GetBotServiceSettings();

        public static async Task Main(string[] args)
        {
            
            IHostBuilder hostBuilder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(_botServiceSettings);
                    services.AddSingleton(SettingsHelper.GetSensitiveSettings(_botServiceSettings));
                    services.AddSingleton<IConnector, MockQuickConnector>();
                    services.AddSingleton<IQuikTerminalService, MockQuickTerminalService>();
                    services.AddSingleton<ISubscriptionsManager, SubscriptionsManager>();
                    services.AddSingleton<ITelegramBotClient, MockTelegramBotClient>();
                    
                    services.AddHostedService<MockQuickTerminalService>();
                    services.AddHostedService<BotService>();
                    
                })
                .UseConsoleLifetime();

            
            await hostBuilder.RunConsoleAsync();
        }

      
    }
}
