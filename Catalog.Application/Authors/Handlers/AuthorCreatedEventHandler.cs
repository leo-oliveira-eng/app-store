using Catalog.Domain.Authors.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Authors.Handlers
{
    public class AuthorCreatedEventHandler : INotificationHandler<AuthorCreatedEvent>
    {
        public Task Handle(AuthorCreatedEvent message, CancellationToken cancellationToken)
        {
            //pretending to do something important

            return Task.CompletedTask;
        }
    }
}
