using BaseEntity.Domain.UnitOfWork;
using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using IdentityServer.Domain.Events.Models;
using IdentityServer.Domain.Repositories;
using IdentityServer.Domain.Services.Contracts;
using IdentityServer.Domain.Services.Dtos;
using Mapster;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Services
{
    public class ChangePasswordService : IChangePasswordService
    {
        private IUserRepository UserRepository { get; }

        private IUnitOfWork UnitOfWork { get; }

        private IDomainEventHandler DomainEventHandler { get; }

        public ChangePasswordService(IUserRepository userRepository, IUnitOfWork unitOfWork, IDomainEventHandler domainEventHandler)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            DomainEventHandler = domainEventHandler ?? throw new ArgumentNullException(nameof(domainEventHandler));
        }

        public async Task<Response> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var response = Response.Create();

            var user = await UserRepository.FindByPasswordRecoverCode(dto.PasswordRecoverCode);

            if (!user.HasValue)
                return response.WithBusinessError("User not found");

            var changePasswordResponse = user.Value.ChangePassword(dto.PasswordRecoverCode, dto.Password, dto.PasswordConfirmation);

            if (changePasswordResponse.HasError)
                return response.WithMessages(changePasswordResponse.Messages);

            await UserRepository.UpdateAsync(user);

            if (!await UnitOfWork.CommitAsync())
                return response.WithCriticalError("Failed to save new user.");

            DomainEventHandler.Raise(new PasswordChangedEvent(user.Value.Adapt<PasswordChangedEventModel>()));

            return response;
        }
    }
}
