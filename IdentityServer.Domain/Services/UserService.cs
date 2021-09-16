using BaseEntity.Domain.UnitOfWork;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Repositories;
using IdentityServer.Domain.Services.Contracts;
using IdentityServer.Domain.Services.Dtos;
using Messages.Core;
using Messages.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Domain.Services
{
    public class UserService : IUserService
    {
        private IUserRepository UserRepository { get; }

        private IUnitOfWork UnitOfWork { get; }

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<Response<User>> CreateAsync(CreateUserDto dto)
        {
            var response = Response<User>.Create();

            if (dto is null)
                return response.WithBusinessError("Invalid data for new user");

            var user = await UserRepository.FindByCPFAsync(dto.Cpf);

            if (user.HasValue)
                return response.WithBusinessError("User already registered.");

            var createUserResponse = User.Create(dto);

            if (createUserResponse.HasError)
                return response.WithMessages(createUserResponse.Messages);

            var addClaimsResponse = AddClaims(dto.Claims, createUserResponse);

            if (addClaimsResponse.HasError)
                return response.WithMessages(addClaimsResponse.Messages);

            await UserRepository.AddAsync(createUserResponse);

            if (!await UnitOfWork.CommitAsync())
                return response.WithCriticalError("Failed to save new user");

            return response.SetValue(createUserResponse);
        }

        private Response AddClaims(List<ClaimDto> dtos, User user)
        {
            var response = Response.Create();

            dtos.ForEach(claim =>
            {
                var addClaimResponse = user.AddClaim(Claim.Create(claim.Type, claim.Value));
                if (addClaimResponse.HasError)
                    response.WithMessages(addClaimResponse.Messages);
            });

            return response;
        }
    }
}
