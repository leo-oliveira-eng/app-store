using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using System.Threading.Tasks;

namespace IdentityServer.ExternalServices.Services
{
    public class RecoverPasswordEmailService : IHandle<RecoverPasswordEvent>
    {
        public async Task Handle(RecoverPasswordEvent args)
        {
            //Send email with recovering code

            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
