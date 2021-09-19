using IdentityServer.Domain.Services.Dtos;
using Messages.Core;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Services.Contracts
{
    public interface IChangePasswordService
    {
        Task<Response> ChangePasswordAsync(ChangePasswordDto dto);
    }
}
