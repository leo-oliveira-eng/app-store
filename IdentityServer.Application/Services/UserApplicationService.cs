using IdentityServer.Application.Services.Contracts;
using IdentityServer.Domain.Services.Contracts;
using IdentityServer.Domain.Services.Dtos;
using IdentityServer.Messaging.Requests;
using IdentityServer.Messaging.Responses;
using Mapster;
using MapsterMapper;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Application.Services
{
    public class UserApplicationService : IUserApplicationService
    {
        private IMapper Mapper { get; }

        private ICreateUserService CreateUserService { get; }

        public UserApplicationService(IMapper mapper, ICreateUserService createUserService)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            CreateUserService = createUserService ?? throw new ArgumentNullException(nameof(createUserService));
        }

        public async Task<Response<CreateUserResponseMessage>> CreateAsync(CreateUserRequestMessage requestMessage)
        {
            var response = Response<CreateUserResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var createUserResponse = await CreateUserService.CreateAsync(Mapper.Map<CreateUserDto>(requestMessage));

            if (createUserResponse.HasError)
                return response.WithMessages(createUserResponse.Messages);

            return response.SetValue(createUserResponse.Adapt<CreateUserResponseMessage>());
        }
    }
}
