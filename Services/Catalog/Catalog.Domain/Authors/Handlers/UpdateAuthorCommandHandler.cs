using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Events;
using Catalog.Domain.Authors.Models;
using Catalog.Domain.Authors.Repositories;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Domain.Authors.Handlers
{
    public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand, Response<Author>>
    {
        private IAuthorRepository AuthorRepository { get; }

        public UpdateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<Response<Author>> Handle(UpdateAuthorCommand command, CancellationToken cancellationToken)
        {
            var response = Response<Author>.Create();

            var validateCommandResponse = command.Validate();

            if (validateCommandResponse.HasError)
                return response.WithMessages(validateCommandResponse.Messages);

            var author = await AuthorRepository.FindAsync(command.Code);

            if (!author.HasValue)
                return response.WithBusinessError("Author not found");

            author.Value.Email = command.Email;
            author.Value.PhoneNumber = command.PhoneNumber;
            author.Value.WebSite = command.WebSite;
            author.Value.BrandLogo = command.BrandLogo;

            author.Value.AddDomainEvent(new UpdatedAuthorEvent(author));

            if (!(await AuthorRepository.UpdateAsync(author)).IsAcknowledged)
                return response.WithCriticalError($"Failed to save author {author.Value.Name}");

            return response.SetValue(author);
        }
    }
}
