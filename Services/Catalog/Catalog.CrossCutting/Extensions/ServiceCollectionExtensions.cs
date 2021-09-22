using Microsoft.Extensions.DependencyInjection;

namespace Catalog.CrossCutting.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDependencyInjector(this IServiceCollection services)
            => DependencyInjector.Configure(services);
    }
}
