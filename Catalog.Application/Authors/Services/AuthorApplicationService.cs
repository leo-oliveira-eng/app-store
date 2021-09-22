using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Application.Authors.Contracts;
using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Models;
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

        public AuthorApplicationService(IMediatorHandler mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            TypeAdapterConfig.GlobalSettings.Default.NameMatchingStrategy(NameMatchingStrategy.IgnoreCase);
        }

        public async Task<Response<CreateAuthorResponseMessage>> CreateAsync(CreateAuthorRequestMessage requestMessage)
        {
            var response = Response<CreateAuthorResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var command = requestMessage.Adapt<CreateAuthorCommand>();

            Response<Author> createAuthorResponse = await Mediator.SendCommand<CreateAuthorCommand, Response<Author>>(command);

            if (createAuthorResponse.HasError)
                return response.WithMessages(createAuthorResponse.Messages);

            return response.SetValue(createAuthorResponse.Data.Value.Adapt<CreateAuthorResponseMessage>());
        }

        public async Task<Response<UpdateAuthorResponseMessage>> UpdateAsync(UpdateAuthorRequestMessage requestMessage)
        {
            var response = Response<UpdateAuthorResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var command = requestMessage.Adapt<UpdateAuthorCommand>();

            var updateAuthorResponse = await Mediator.SendCommand<UpdateAuthorCommand, Response<Author>>(command);

            if (updateAuthorResponse.HasError)
                return response.WithMessages(updateAuthorResponse.Messages);

            return response.SetValue(updateAuthorResponse.Data.Value.Adapt<UpdateAuthorResponseMessage>());
        }
    }
}
