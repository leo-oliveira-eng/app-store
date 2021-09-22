using BaseEntity.Domain.Messaging;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using System;

namespace Catalog.Domain.Authors.Commands
{
    public class DeleteAuthorCommand : Command, IRequest<Response>
    {
        public Guid Id { get; set; }

        public DeleteAuthorCommand(Guid id)
        {
            Id = id;
        }

        public override Response Validate()
        {
            var response = Response.Create();

            if (Id.Equals(Guid.Empty))
                response.WithBusinessError(nameof(Id), $"{nameof(Id)} is invalid");

            return response;
        }
    }
}
