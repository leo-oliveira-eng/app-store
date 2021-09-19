using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IdentityServer.ExternalServices.Services
{
    public class RecoverPasswordEmailService : IHandle<RecoverPasswordEvent>
    {
        private readonly ILogger<RecoverPasswordEmailService> _logger;

        public RecoverPasswordEmailService(ILogger<RecoverPasswordEmailService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(RecoverPasswordEvent args)
        {
            _logger.LogInformation("[Identity Server] Iniciando evento {DomainEvent}", args.GetType().Name);

            //Send email with recovering code

            _logger.LogInformation("[Identity Server] Evento {DomainEvent} conclído", args.GetType().Name);

            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
