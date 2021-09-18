using BaseEntity.Domain.UnitOfWork;
using IdentityServer.Application.Services;
using IdentityServer.Application.Services.Contracts;
using IdentityServer.Data.Context;
using IdentityServer.Data.Repositories;
using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using IdentityServer.Domain.Repositories;
using IdentityServer.Domain.Services;
using IdentityServer.Domain.Services.Contracts;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.CrossCutting
{
    public static class DependencyInjector
    {
        public static void Configure(IServiceCollection services)
        {
            #region ' Unit of Work '

            services.AddScoped<IUnitOfWork, UnitOfWork<IdentityContext>>();

            #endregion

            #region User

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserApplicationService, UserApplicationService>();
            services.AddTransient<ICreateUserService, CreateUserService>();
            services.AddTransient<IRecoverPasswordService, RecoverPasswordService>();

            #endregion

            #region Handlers

            services.AddScoped<IDomainEventHandler, DomainEventHandler>();

            #endregion
        }
    }
}
