using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Domain.Authors.Repositories;
using Catalog.Infrastructure.Context.Contracts;
using Catalog.Infrastructure.Shared.Repositories;
using Model = Catalog.Domain.Authors.Models;

namespace Catalog.Infrastructure.Author.Repositories
{
    public class AuthorRepository : BaseRepository<Model.Author>, IAuthorRepository
    {
        public AuthorRepository(IMongoContext context, IMediatorHandler mediatorHandler)
            : base(context, mediatorHandler) { }
    }
}
