using System.IO;
using Microsoft.Extensions.Configuration;

namespace TelegramAssistant.Settings
{
    public static class SettingsHelper
    {
        internal static SensitiveSettings GetSensitiveSettings(BotServiceSettings appSettings)
        {
            var sensitiveConfigFile = appSettings.SensitiveConfigFile;
            var sensConfBuilder = new ConfigurationBuilder()
                .AddJsonFile(sensitiveConfigFile);
            IConfiguration sensConfiguration = sensConfBuilder.Build();
            return new SettingsBuilder<SensitiveSettings>(sensConfiguration).Build();
        }

        internal static BotServiceSettings GetBotServiceSettings()
        {
            var settingsDir = Path.Combine(Directory.GetCurrentDirectory(), "Settings");
            var configLocation = Path.Combine(settingsDir, "AppSettings.json");
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile(configLocation);

            IConfiguration configuration = configurationBuilder.Build();
            return new SettingsBuilder<BotServiceSettings>(configuration).Build();
        }
    }


    public class SettingsBuilder<T> where T: class, new()
    {
        private readonly IConfiguration _configuration;

        public SettingsBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public T Build()
        {
            var genericConfig = new T();
            _configuration.Bind(genericConfig);
            return genericConfig;
        }
    }
}
