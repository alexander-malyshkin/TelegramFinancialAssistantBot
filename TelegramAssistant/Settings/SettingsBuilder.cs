using Microsoft.Extensions.Configuration;

namespace TelegramAssistant.Settings
{
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
