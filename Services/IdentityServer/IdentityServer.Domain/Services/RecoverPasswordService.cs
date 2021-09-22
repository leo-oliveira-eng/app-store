using BaseEntity.Domain.UnitOfWork;
using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using IdentityServer.Domain.Events.Models;
using IdentityServer.Domain.Repositories;
using IdentityServer.Domain.Services.Contracts;
using Mapster;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Threading.Tasks;
using Valuables.Utils;

namespace IdentityServer.Domain.Services
{
    public class RecoverPasswordService : IRecoverPasswordService
    {
        private IUserRepository UserRepository { get; }

        private IUnitOfWork UnitOfWork { get; }

        private IDomainEventHandler DomainEventHandler { get; }

        public RecoverPasswordService(IUserRepository userRepository, IUnitOfWork unitOfWork, IDomainEventHandler domainEventHandler)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            DomainEventHandler = domainEventHandler ?? throw new ArgumentNullException(nameof(domainEventHandler));
        }

        public async Task<Response> RecoverPasswordAsync(string cpf)
        {
            var response = Response.Create();

            var user = await UserRepository.FindByCPFAsync(CPF.Clear(cpf));

            if (!user.HasValue)
                return response.WithBusinessError("User not found");

            user.Value.GeneratePasswordRecoverCode();

            await UserRepository.UpdateAsync(user);

            if (!await UnitOfWork.CommitAsync())
                return response.WithCriticalError("Failed to save new user.");

            DomainEventHandler.Raise(new RecoverPasswordEvent(user.Value.Adapt<RecoverPasswordEventModel>()));

            return response;
        }
    }
}
