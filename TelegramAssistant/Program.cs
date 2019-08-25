using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramAssistant.Contracts;
using TelegramAssistant.db_adapter;
using TelegramAssistant.ExchangeRateProviders;
using TelegramAssistant.NotificationSubscribers;
using TelegramAssistant.Services;
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

                    services.AddSingleton<QuikTerminalService>();

               //     services.AddSingleton<trading_der_dbContext>();

                    services.AddSingleton<ISubscriptionsManager, SubscriptionsManager>();
                    
                    services.AddHostedService<BotService>();
                   // services.AddHostedService<trading_der_dbContext>();
                    services.AddHostedService<QuikTerminalService>();

                   
                    
                })
                .UseConsoleLifetime();

            
            await hostBuilder.RunConsoleAsync();
        }

      
    }
}
