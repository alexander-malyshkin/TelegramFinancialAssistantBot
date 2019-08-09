using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Security.Authentication.ExtendedProtection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Telegram.Bot;
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
            //IServiceCollection sc = new ServiceCollection();
            //sc.AddHostedService<QueuedHostedService>();
            //sc.AddSingleton<IBackgroundTaskQueue, BackgroundTaskQueue>();
            //sc.AddSingleton(typeof(INotificationSubscriber), 
            //    new NotificationSubscriber(new MockSberbankExchangeRatesProvider()));

            var sc2 = new ServiceContainer();
            sc2.AddService(typeof(QueuedHostedService), new QueuedHostedService(new BackgroundTaskQueue()));
            sc2.AddService(typeof(IBackgroundTaskQueue), new BackgroundTaskQueue());
            sc2.AddService(typeof(INotificationSubscriber), new NotificationSubscriber(new MockExchangeRatesProvider()));
            
            var app = new App(sc2);
            Console.ReadLine();
            app.StopReceiving();
        }
    }
}
