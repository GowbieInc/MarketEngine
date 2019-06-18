using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MarketEngine.Infra
{
    public class Bootstrapper
    {
        private static IServiceCollection _services;
        public static void RegisterAllServices(IServiceCollection services)
        {
            _services = services;
            RegisterExternalDependencies();
            RegisterServices();
            RegisterRepositories();
        }

        private static void RegisterExternalDependencies()
        {
            _services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        private static void RegisterServices()
        {

        }
        
        private static void RegisterRepositories()
        {

        }
    }
}
    