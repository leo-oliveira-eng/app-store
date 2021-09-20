using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Common.Configurations;
using Catalog.Domain.Authors.Repositories;
using Catalog.Infrastructure.Author.Documents;
using Catalog.Infrastructure.Shared;
using Mapster;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Linq;
using System.Threading.Tasks;
using Model = Catalog.Domain.Authors.Models;

namespace Catalog.Infrastructure.Author.Repositories
{
    public class AuthorRepository : MongoDbBaseRepository, IAuthorRepository
    {
        private readonly IMongoCollection<AuthorDocument> _colection;

        private readonly IMediatorHandler _mediatorHandler;

        public AuthorRepository(IOptions<MongoDbConfiguration> mongoDbConfiguration, IMediatorHandler mediatorHandler)
            : base(mongoDbConfiguration)
        {
            _colection = Database.GetCollection<AuthorDocument>(mongoDbConfiguration.Value.CollectionNames.Author);
            _mediatorHandler = mediatorHandler ?? throw new ArgumentNullException(nameof(mediatorHandler));
        }

        public async Task AddAsync(Model.Author author)
        {
            await _colection.InsertOneAsync(author.Adapt<AuthorDocument>());

            await PublishDomainEvents(author);
        }

        private async Task PublishDomainEvents(Model.Author author)
        {
            foreach (var domainEvent in author.DomainEvents.Where(e => !e.IsPublished).ToList())
            {
                if (domainEvent == null) break;

                await _mediatorHandler.PublishEvent(domainEvent);

                domainEvent.IsPublished = true;
            }
        }
    }
}
