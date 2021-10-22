using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Application.Authors.Contracts;
using Catalog.Application.Authors.Handlers;
using Catalog.Application.Authors.Services;
using Catalog.CrossCutting.Bus;
using Catalog.Domain.Apps.Commands;
using Catalog.Domain.Apps.Handlers;
using Catalog.Domain.Apps.Models;
using Catalog.Domain.Apps.Repositories;
using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Events;
using Catalog.Domain.Authors.Handlers;
using Catalog.Domain.Authors.Models;
using Catalog.Domain.Authors.Repositories;
using Catalog.Infrastructure.App.Repositories;
using Catalog.Infrastructure.Author.Repositories;
using Catalog.Infrastructure.Context;
using Catalog.Infrastructure.Context.Contracts;
using Catalog.Infrastructure.Mappings;
using MediatR;
using Messages.Core;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.CrossCutting
{
    public static class DependencyInjector
    {
        public static void Configure(IServiceCollection services)
        {
            #region Data

            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IAppRepository, AppRepository>();
            services.AddScoped<IMongoContext, MongoContext>();
            EntityMapping.MapEntity();

            #endregion

            #region Handlers

            services.AddScoped<IRequestHandler<CreateAuthorCommand, Response<Author>>, CreateAuthorCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateAuthorCommand, Response<Author>>, UpdateAuthorCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteAuthorCommand, Response>, DeleteAuthorCommandHandler>();
            services.AddScoped<IRequestHandler<CreateAppCommand, Response<App>>, CreateAppCommandHandler>();
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            services.AddScoped<INotificationHandler<AuthorCreatedEvent>, AuthorCreatedEventHandler>();

            #endregion

            #region Services

            services.AddTransient<IAuthorApplicationService, AuthorApplicationService>();

            #endregion
        }
    }
}
