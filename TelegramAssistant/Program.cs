using System;
using System.ComponentModel.Design;
using Microsoft.Extensions.Hosting;
using TelegramAssistant.Contracts;
using TelegramAssistant.ExchangeRateProviders;
using TelegramAssistant.NotificationSubscribers;
using TelegramAssistant.Services;

namespace TelegramAssistant
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var serviceContainer = new ServiceContainer();
            serviceContainer.AddService(typeof(IExchangeRatesProvider), new MockExchangeRatesProvider());
            serviceContainer.AddService(typeof(INotificationSubscriber), new NotificationSubscriber(new MockExchangeRatesProvider()));
            
            var app = new App(serviceContainer);
            serviceContainer.AddService(typeof(IHostedService), 
                new TimedHostedNotificationService(app.AppSettings.NotificationSettings?.IntervalMilliseconds ?? 1000,
                    app.AppSettings.NotificationSettings?.DueTimeSpanSeconds ?? 0,
                    (IExchangeRatesProvider) serviceContainer.GetService(typeof(IExchangeRatesProvider))));
            Console.ReadLine();
            app.StopReceiving();
        }
    }
}
