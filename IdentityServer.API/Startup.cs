using ExpressionDebugger;
using IdentityServer.API.Extensions;
using IdentityServer.API.Services;
using IdentityServer.CrossCutting.Extensions;
using IdentityServer.Data.Context;
using IdentityServer.Messaging.Requests;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq.Expressions;
using Valuables.Utils;

namespace IdentityServer.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(Configuration);

            services.AddControllers();

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnectionString")));

            services.ConfigureDependencyInjector();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
                .AddProfileService<ProfileService>();

            services.AddSwagger();

            services.AddSingleton(GetConfiguredMappingConfig());

            services.AddScoped<IMapper, ServiceMapper>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseIdentityServer();
        }

        public static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<CreateAddressRequestMessage, Address>()
                .IgnoreNullValues(true)
                .ConstructUsing(src => Address.Create(src.Cep, src.Street, src.Neighborhood, src.Number, src.City, src.UF, src.Complement).Data.Value)
                .Compile();

            return config;
        }
    }
}
