using IdentityServer.Domain.Repositories;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Secutiry = System.Security.Claims;

namespace IdentityServer.API.Services
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private IUserRepository UserRepository { get; }

        public ResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            UserRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var user = await UserRepository.FindByCPFAsync(context.UserName);

            if (!user.HasValue)
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "User does not exist.");
                return;
            }

            if (!user.Value.Password.Equals(context.Password.Sha256()))
            {
                context.Result = new GrantValidationResult(TokenRequestErrors.UnauthorizedClient);
                return;
            }

            context.Result = new GrantValidationResult(user.Value.Id.ToString(), "custom", user.Value.Claims.Select(c => c.Claim)
                        .Select(c => new Secutiry.Claim(c.Type, c.Value)));
        }
    }
}
