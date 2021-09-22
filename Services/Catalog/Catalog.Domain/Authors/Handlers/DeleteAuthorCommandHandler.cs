using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Repositories;
using MediatR;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.Domain.Authors.Handlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, Response>
    {
        private IAuthorRepository AuthorRepository { get; }

        public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
        {
            AuthorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));
        }

        public async Task<Response> Handle(DeleteAuthorCommand command, CancellationToken cancellationToken)
        {
            var response = Response.Create();

            var validateCommandResponse = command.Validate();

            if (validateCommandResponse.HasError)
                return validateCommandResponse;

            var author = await AuthorRepository.FindAsync(command.Id);

            if (!author.HasValue)
                return response.WithBusinessError("Author not found");

            if (!(await AuthorRepository.RemoveAsync(author)).IsAcknowledged)
                return response.WithCriticalError("Failed to delete author");

            return response;
        }
    }
}
