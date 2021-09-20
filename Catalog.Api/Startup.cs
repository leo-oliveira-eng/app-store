using Catalog.Api.Extensions;
using Catalog.Api.Middlewares;
using Catalog.CrossCutting.Extensions;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Reflection;

namespace Catalog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            var elasticUri = Configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true,
                })
            .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(Configuration);

            services.AddControllers();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.ConfigureDependencyInjector();

            services.AddHealthChecks().AddMongoDb(Configuration["MongoDB:ConnectionString"]);

            services.AddTransient<ExceptionHandlingMiddleware>();

            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                app.UseSwagger().UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "App Store - Identity Server");
                    c.RoutePrefix = string.Empty;
                });
            }

            loggerFactory.AddSerilog();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseHealthChecksConfig();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
