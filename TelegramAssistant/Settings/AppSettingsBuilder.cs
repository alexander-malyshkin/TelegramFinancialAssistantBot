using Microsoft.Extensions.Configuration;

namespace TelegramAssistant.Settings
{
    public class AppSettingsBuilder
    {
        private readonly IConfiguration _configuration;

        public AppSettingsBuilder(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AppSettings Build()
        {
            var appSettings = new AppSettings();
            _configuration.Bind(appSettings);
            return appSettings;
        }
    }
}
