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

        private IRecoverPasswordService RecoverPasswordService { get; }

        private IChangePasswordService ChangePasswordService { get; }

        public UserApplicationService(IMapper mapper
            , ICreateUserService createUserService
            , IRecoverPasswordService recoverPasswordService
            , IChangePasswordService changePasswordService)
        {
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            CreateUserService = createUserService ?? throw new ArgumentNullException(nameof(createUserService));
            RecoverPasswordService = recoverPasswordService ?? throw new ArgumentNullException(nameof(recoverPasswordService));
            ChangePasswordService = changePasswordService ?? throw new ArgumentNullException(nameof(changePasswordService));
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

        public async Task<Response<RecoverPasswordResponseMessage>> RecoverPasswordAsync(RecoverPasswordRequestMessage requestMessage)
        {
            var response = Response<RecoverPasswordResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var recoverPasswordResponse = await RecoverPasswordService.RecoverPasswordAsync(requestMessage.Cpf);

            if (recoverPasswordResponse.HasError)
                return response.WithMessages(recoverPasswordResponse.Messages);

            return response;
        }

        public async Task<Response<ChangePasswordResponseMessage>> ChangePasswordAsync(ChangePasswordRequestMessage requestMessage)
        {
            var response = Response<ChangePasswordResponseMessage>.Create();

            if (requestMessage is null)
                return response.WithBusinessError("Request data is invalid");

            var changePasswordResponse = await ChangePasswordService.ChangePasswordAsync(requestMessage.Adapt<ChangePasswordDto>());

            if (changePasswordResponse.HasError)
                return response.WithMessages(changePasswordResponse.Messages);

            return response;
        }
    }
}
