using IdentityServer.Data.Context;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Repositories;
using Infrastructure.Repositories;
using Messages.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Data.Repositories
{
    public class UserRepository : Repository<User, IdentityContext>, IUserRepository
    {
        public UserRepository(IdentityContext context) : base(context) { }

        public async Task<Maybe<User>> FindByCPFAsync(string cpf)
            => await DbSet.Include(x => x.Claims)
                .ThenInclude(x => x.Claim)
                .FirstOrDefaultAsync(user => user.CPF.Equals(cpf));

        public async Task<Maybe<User>> FindByPasswordRecoverCode(Guid code)
            => await DbSet.FirstOrDefaultAsync(x => x.PasswordRecoverCode.Equals(code));
    }
}
