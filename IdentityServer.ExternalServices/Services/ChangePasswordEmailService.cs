using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using System.Threading.Tasks;

namespace IdentityServer.ExternalServices.Services
{
    public class ChangePasswordEmailService : IHandle<PasswordChangedEvent>
    {
        public async Task Handle(PasswordChangedEvent args)
        {
            //send email to inform that password has changed

            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
