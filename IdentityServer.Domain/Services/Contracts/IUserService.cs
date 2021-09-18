using IdentityServer.Domain.Services.Dtos;
using Messages.Core;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Services.Contracts
{
    public interface IUserService
    {
        Task<Response> RecoverPasswordAsync(string cpf);

        Task<Response> ChangePasswordAsync(ChangePasswordDto dto);
    }
}
