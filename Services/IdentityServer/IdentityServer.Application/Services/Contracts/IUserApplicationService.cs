using IdentityServer.Messaging.Requests;
using IdentityServer.Messaging.Responses;
using Messages.Core;
using System.Threading.Tasks;

namespace IdentityServer.Application.Services.Contracts
{
    public interface IUserApplicationService
    {
        Task<Response<CreateUserResponseMessage>> CreateAsync(CreateUserRequestMessage requestMessage);

        Task<Response<RecoverPasswordResponseMessage>> RecoverPasswordAsync(RecoverPasswordRequestMessage requestMessage);

        Task<Response<ChangePasswordResponseMessage>> ChangePasswordAsync(ChangePasswordRequestMessage requestMessage);
    }
}
