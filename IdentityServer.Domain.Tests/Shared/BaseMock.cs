using IdentityServer.Domain.Models;
using Valuables.Utils;

namespace IdentityServer.Domain.Tests.Shared
{
    public class BaseMock
    {
        public Claim ClaimFake(string type = null, string value = null)
            => Claim.Create(type ?? "Any Type", value ?? "Any Value").Data.Value;

        public Email EmailFake(string address = null)
            => Email.Create(address ?? "any@nothing.com");

        public User UserFake(string cpf = null, string name = null, string password = null, string email = null)
            => User.Create(cpf ?? "783.297.220-31"
                , name ?? "Any Name"
                , password ?? "Abc123"
                , email ?? "any@nothing.com")
            .Data.Value;
    }
}
