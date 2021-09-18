using ExpressionDebugger;
using IdentityServer.CrossCutting.Extensions;
using IdentityServer.Data.Context;
using IdentityServer.Domain.Services.Dtos;
using IdentityServer.Messaging.Requests;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
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
            services.AddControllers();

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("IdentityConnectionString")));

            services.ConfigureDependencyInjector();

            services.AddSingleton(GetConfiguredMappingConfig());

            services.AddScoped<IMapper, ServiceMapper>();
        }

        private static TypeAdapterConfig GetConfiguredMappingConfig()
        {
            var config = new TypeAdapterConfig
            {
                Compiler = exp => exp.CompileWithDebugInfo(
                    new ExpressionCompilationOptions
                    {
                        EmitFile = true,
                        ThrowOnFailedCompilation = true
                    })
            };

            config.NewConfig<CreateAddressRequestMessage, Address>()
                .IgnoreNullValues(true)
                .ConstructUsing(src => Address.Create(src.Cep, src.Street, src.Neighborhood, src.Number, src.City, src.UF, src.Complement).Data.Value)
                .Compile();

            return config;
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
