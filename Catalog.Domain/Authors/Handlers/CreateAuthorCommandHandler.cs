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
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Response<Author>>
    {
        IAuthorRepository AuthorRepository { get; }

        public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<Response<Author>> Handle(CreateAuthorCommand command, CancellationToken cancellationToken)
        {
            var response = Response<Author>.Create();

            var validateCommandResponse = command.Validate();

            if (validateCommandResponse.HasError)
                return response.WithMessages(validateCommandResponse.Messages);

            var author = new Author(command.Name, command.CNPJ, command.PhoneNumber, command.Email,
                command.WebSite, command.BrandLogo);

            author.AddDomainEvent(new AuthorCreatedEvent(author));

            await AuthorRepository.AddAsync(author);            

            return response.SetValue(author);
        }
    }
}
