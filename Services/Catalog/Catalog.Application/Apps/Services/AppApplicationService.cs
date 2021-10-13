using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Application.Apps.Contracts;
using Catalog.Domain.Apps.Commands;
using Catalog.Domain.Apps.Models;
using Catalog.Messages.Requests;
using Catalog.Messages.Responses;
using Mapster;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace Catalog.Application.Apps.Services
{
    public class AppApplicationService : IAppApplicationService
    {
        private IMediatorHandler Mediator { get; }

        public AppApplicationService(IMediatorHandler mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<Response<AppResponseMessage>> CreateAsync(CreateAppRequestMessage requestMessage)
        {
            var response = Response<AppResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var command = requestMessage.Adapt<CreateAppCommand>();

            var createAppResponse = await Mediator.SendCommand<CreateAppCommand, Response<App>>(command);

            if (createAppResponse.HasError)
                return response.WithMessages(createAppResponse.Messages);

            return response.SetValue(createAppResponse.Data.Value.Adapt<AppResponseMessage>());
        }
    }
}
