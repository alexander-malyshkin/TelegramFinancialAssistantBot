using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;
using TelegramAssistant.Settings;

namespace TelegramAssistant
{
    public class App
    {
        public AppSettings AppSettings { get; set; }

        public App()
        {
            string configLocation = Path.Combine("Settings", "AppSettings.json");
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), configLocation));

            IConfiguration configuration = configurationBuilder.Build();
            AppSettings = (new AppSettingsBuilder(configuration)).Build();
        }
    }
}
