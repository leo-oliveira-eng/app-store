﻿using BaseEntity.Domain.UnitOfWork;
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

            #endregion

            #region Handlers

            services.AddScoped<IDomainEventHandler, DomainEventHandler>();

            #endregion
        }
    }
}
