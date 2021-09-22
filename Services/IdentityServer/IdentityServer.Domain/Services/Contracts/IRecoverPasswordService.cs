using Messages.Core;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Services.Contracts
{
    public interface IRecoverPasswordService
    {
        Task<Response> RecoverPasswordAsync(string cpf);
    }
}
