using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Application.Authors.Contracts;
using Catalog.Application.Authors.Handlers;
using Catalog.Application.Authors.Services;
using Catalog.CrossCutting.Bus;
using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Events;
using Catalog.Domain.Authors.Handlers;
using Catalog.Domain.Authors.Models;
using Catalog.Domain.Authors.Repositories;
using Catalog.Infrastructure.Author.Repositories;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Context.Contracts;
using MediatR;
using Messages.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.CrossCutting
{
    public static class DependencyInjector
    {
        public static void Configure(IServiceCollection services)
        {
            #region Domain - Commands

            #endregion

            #region Data

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IMongoContext, MongoContext>();

            #endregion

            #region Handlers

            services.AddScoped<IRequestHandler<CreateAuthorCommand, Response<Author>>, CreateAuthorCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAuthorCommand, Response<Author>>, UpdateAuthorCommandHandler>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<AuthorCreatedEvent>, AuthorCreatedEventHandler>();

            #endregion

            #region Services

            services.AddTransient<IAuthorApplicationService, AuthorApplicationService>();

            #endregion
        }
    }
}
