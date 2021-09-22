using FluentAssertions;
using IdentityServer.Domain.Tests.Shared;
using Messages.Core;
using Messages.Core.Enums;
using Xunit;

namespace IdentityServer.Domain.Tests.UserTests
{
    public class AddClaimUnitTests : BaseMock
    {
        [Fact]
        public void AddClaimUser_ShouldReturnSuccessWithValidParams()
        {
            var user = UserFake();
            var claim = ClaimFake();

            Response response = user.AddClaim(claim);

            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            user.Claims.Should().Contain(c => c.Claim.Equals(claim));
        }

        [Fact]
        public void AddClaimUser_ShouldReturnSuccess_SameClaim()
        {
            var user = UserFake();
            var claim = ClaimFake();
            user.AddClaim(claim);

            Response response = user.AddClaim(claim);

            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            user.Claims.Should().Contain(c => c.Claim.Equals(claim));
        }

        [Fact]
        public void AddClaimUser_ShouldReturnError_InvalidClaim()
        {
            var user = UserFake();
            var claim = ClaimFake(type: string.Empty);

            Response response = user.AddClaim(claim);

            response.HasError.Should().BeTrue();
            response.Messages.Should().NotBeEmpty();
            response.Messages.Count.Should().Be(1);
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
        }
    }
}
