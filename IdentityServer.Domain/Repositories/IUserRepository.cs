using BaseEntity.Domain.Repositories;
using IdentityServer.Domain.Models;
using Messages.Core;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<Maybe<User>> FindByCPFAsync(string cpf);
    }
}
