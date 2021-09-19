using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.CrossCutting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDependencyInjector(this IServiceCollection services)
            => DependencyInjector.Configure(services);
    }
}
