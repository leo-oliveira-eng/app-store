using Messages.Core;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catalog.Domain.Common.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);

        Task<Maybe<TEntity>> FindAsync(Guid code);

        Task<List<TEntity>> GetAllAsync();

        Task<UpdateResult> RemoveAsync(TEntity entity);

        Task<ReplaceOneResult> UpdateAsync(TEntity entity);
    }
}
