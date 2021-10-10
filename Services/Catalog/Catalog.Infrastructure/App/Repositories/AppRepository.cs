using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Domain.Apps.Repositories;
using Catalog.Infrastructure.Context.Contracts;
using Catalog.Infrastructure.Shared.Repositories;
using Model = Catalog.Domain.Apps.Models;

namespace Catalog.Infrastructure.App.Repositories
{
    public class AppRepository : BaseRepository<Model.App>, IAppRepository
    {
        public AppRepository(IMongoContext context, IMediatorHandler mediatorHandler)
            : base(context, mediatorHandler) { }
    }
}
