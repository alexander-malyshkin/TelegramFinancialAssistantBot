using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TelegramAssistant.Contracts;
using TelegramAssistant.Events.Handlers;
using TelegramAssistant.ExchangeRateProviders;
using TelegramAssistant.NotificationSubscribers;
using TelegramAssistant.Services;
using TelegramAssistant.Settings;

namespace TelegramAssistant
{
    public static class Program
    {
        private static AppSettings _appSettings;

        public static async Task Main(string[] args)
        {
            //var serviceContainer = new ServiceContainer();
            //serviceContainer.AddService(typeof(IExchangeRatesProvider), new MockExchangeRatesProvider());
            //serviceContainer.AddService(typeof(INotificationSubscriber), new NotificationSubscriber(new MockExchangeRatesProvider()));

            //var app = new App(serviceContainer);
            //serviceContainer.AddService(typeof(IHostedService), 
            //    new TimedHostedNotificationService(app.AppSettings.NotificationSettings?.IntervalMilliseconds ?? 1000,
            //        app.AppSettings.NotificationSettings?.DueTimeSpanSeconds ?? 0,
            //        (IExchangeRatesProvider) serviceContainer.GetService(typeof(IExchangeRatesProvider))));
            //Console.ReadLine();
            //app.StopReceiving();

            _appSettings = GetAppSettings();

            IHostBuilder hostBuilder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton(_appSettings);
                    services.AddSingleton(GetSensitiveSettings(_appSettings));
                    services.AddSingleton(_appSettings.NotificationSettings);
                    services.AddSingleton<IExchangeRatesProvider, MockExchangeRatesProvider>();
                    services.AddSingleton<ISubscriptionsManager, SubscriptionsManager>();

                    services.AddScoped<App>();
                    services.AddScoped<IAssetValueChangedEventHandler, AssetValueChangedEventHandler>();

                    services.AddHostedService<TimedHostedNotificationService>();
                    services.AddHostedService<CommandProcessingService>();
                })
                .UseConsoleLifetime();

            //var h = hostBuilder.Build();
            //var handler = (AssetValueChangedEventHandler) h.Services.GetService(typeof(AssetValueChangedEventHandler));

            await hostBuilder.RunConsoleAsync();
        }

        private static SensitiveSettings GetSensitiveSettings(AppSettings appSettings)
        {
            var sensitiveConfigFile = appSettings.SensitiveConfigFile;
            var sensConfBuilder = new ConfigurationBuilder()
                .AddJsonFile(sensitiveConfigFile);
            IConfiguration sensConfiguration = sensConfBuilder.Build();
            return new SettingsBuilder<SensitiveSettings>(sensConfiguration).Build();
        }

        private static AppSettings GetAppSettings()
        {
            var settingsDir = Path.Combine(Directory.GetCurrentDirectory(), "Settings");
            var configLocation = Path.Combine(settingsDir, "AppSettings.json");
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(configLocation);

            IConfiguration configuration = configurationBuilder.Build();
            return new SettingsBuilder<AppSettings>(configuration).Build();
        }
    }
}
