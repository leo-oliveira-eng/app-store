using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IdentityServer.ExternalServices.Services
{
    public class ChangePasswordEmailService : IHandle<PasswordChangedEvent>
    {
        private readonly ILogger<PasswordChangedEvent> _logger;

        public ChangePasswordEmailService(ILogger<PasswordChangedEvent> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Handle(PasswordChangedEvent args)
        {
            _logger.LogInformation("[Identity Server] Iniciando evento {DomainEvent}", args.GetType().Name);
            //send email to inform that password has changed

            _logger.LogInformation("[Identity Server] Evento {DomainEvent} conclído", args.GetType().Name);
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}
