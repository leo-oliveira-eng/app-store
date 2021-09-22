using Catalog.Domain.Authors.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Application.Authors.Handlers
{
    public class UpdatedAuthorEventHandler : INotificationHandler<UpdatedAuthorEvent>
    {
        public Task Handle(UpdatedAuthorEvent message, CancellationToken cancellationToken)
        {
            //pretending to do another important thing

            return Task.CompletedTask;
        }
    }
}
