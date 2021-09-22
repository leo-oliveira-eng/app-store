using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;

namespace Catalog.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureCors(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var allowedHosts = configuration.GetSection("AllowedHosts")
                .AsEnumerable()
                .Where(i => i.Value != null)
                .Select(i => i.Value)
                .ToArray();

            serviceCollection.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .WithOrigins(allowedHosts));
            });
        }

        public static void AddSwagger(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.CustomSchemaIds(x => x.FullName);
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "App Store - Identity Server",
                    Contact = new OpenApiContact
                    {
                        Name = "App Store",
                        Email = "contato@appstore",
                        Url = new Uri("https://appstore.com")
                    },
                    Version = "1.0.0"
                });
            });
        }
    }
}
