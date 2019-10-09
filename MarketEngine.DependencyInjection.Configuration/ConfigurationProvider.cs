using Microsoft.Extensions.Configuration;

namespace MarketEngine.DependencyInjection.Configuration
{
    public static class ConfigurationProvider
    {
        private static IConfiguration configuration;

        public static void BuildProvider(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public static T GetOptions<T>() where T: class, new()
        {
            var options = new T();
            configuration.GetSection(typeof(T).Name).Bind(options);
            return options;
        }
    }
}
