using System;
using System.ComponentModel.Design;
using TelegramAssistant.Contracts;
using TelegramAssistant.ExchangeRateProviders;
using TelegramAssistant.NotificationSubscribers;

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
            
            sc2.AddService(typeof(IExchangeRatesProvider), new MockExchangeRatesProvider());
            sc2.AddService(typeof(INotificationSubscriber), new NotificationSubscriber(new MockExchangeRatesProvider()));
            
            var app = new App(sc2);
            Console.ReadLine();
            app.StopReceiving();
        }
    }
}
