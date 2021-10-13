using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Application.Authors.Contracts;
using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Models;
using Catalog.Domain.Authors.Repositories;
using Catalog.Messages.Requests;
using Catalog.Messages.Responses;
using Mapster;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace Catalog.Application.Authors.Services
{
    public class AuthorApplicationService : IAuthorApplicationService
    {
        private IMediatorHandler Mediator { get; }

        private IAuthorRepository AuthorRepository { get; }

        public AuthorApplicationService(IMediatorHandler mediator, IAuthorRepository authorRepository)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            AuthorRepository = authorRepository ?? throw new ArgumentNullException(nameof(authorRepository));

            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
        }

        public async Task<Response<AuthorResponseMessage>> CreateAsync(CreateAuthorRequestMessage requestMessage)
        {
            var response = Response<AuthorResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var command = requestMessage.Adapt<CreateAuthorCommand>();

            var createAuthorResponse = await Mediator.SendCommand<CreateAuthorCommand, Response<Author>>(command);

            if (createAuthorResponse.HasError)
                return response.WithMessages(createAuthorResponse.Messages);

            return response.SetValue(createAuthorResponse.Data.Value.Adapt<AuthorResponseMessage>());
        }

        public async Task<Response<AuthorResponseMessage>> UpdateAsync(UpdateAuthorRequestMessage requestMessage, Guid id)
        {
            var response = Response<AuthorResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var command = requestMessage.Adapt<UpdateAuthorCommand>();

            var updateAuthorResponse = await Mediator.SendCommand<UpdateAuthorCommand, Response<Author>>(command);

            if (updateAuthorResponse.HasError)
                return response.WithMessages(updateAuthorResponse.Messages);

            return response.SetValue(updateAuthorResponse.Data.Value.Adapt<AuthorResponseMessage>());
        }

        public async Task<Response<AuthorResponseMessage>> FindAsync(Guid id)
        {
            var author = await AuthorRepository.FindAsync(id);

            if (!author.HasValue)
                return Response<AuthorResponseMessage>.Create().WithBusinessError("Author not found");

            return author.Value.Adapt<AuthorResponseMessage>();
        }

        public async Task<Response<DeleteAuthorResponseMessage>> DeleteAsync(Guid id)
        {
            var deleteAuthorResponse = await Mediator.SendCommand<DeleteAuthorCommand, Response>(new DeleteAuthorCommand(id));

            return Response<DeleteAuthorResponseMessage>.Create().WithMessages(deleteAuthorResponse.Messages);
        }
    }
}
