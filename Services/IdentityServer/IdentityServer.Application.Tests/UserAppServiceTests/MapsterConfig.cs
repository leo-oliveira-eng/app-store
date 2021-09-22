using IdentityServer.CrossCutting.Extensions;
using IdentityServer.Messaging.Requests;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Valuables.Utils;

namespace IdentityServer.Application.Tests.UserAppServiceTests
{
    public static class MapsterConfig
    {
        public static IServiceScope CreateScope()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<CreateAddressRequestMessage, Address>()
                .IgnoreNullValues(true)
                .ConstructUsing(src => Address.Create(src.Cep, src.Street, src.Neighborhood, src.Number, src.City, src.UF, src.Complement).Data.Value)
                .Compile();

            IServiceCollection services = new ServiceCollection();
            services.ConfigureDependencyInjector();
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider.CreateScope();
        }
    }
}
