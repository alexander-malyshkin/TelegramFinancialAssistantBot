using System;

namespace TelegramAssistant
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var app = new App();
            Console.ReadLine();
            app.StopReceiving();
        }
    }
}
