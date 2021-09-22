using FluentAssertions;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Tests.Shared;
using Messages.Core.Enums;
using Xunit;

namespace IdentityServer.Domain.Tests.UserClaimTests
{
    public class CreateUnitTests : BaseMock
    {
        [Fact]
        public void CreateUserClaim_ShoudReturnSuccess_ValidParameters()
        {
            var response = UserClaim.Create(UserFake(), ClaimFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(UserClaim));
        }

        [Fact]
        public void CreateUserClaim_ShouldReturnError_UserIsInvalid()
        {
            var response = UserClaim.Create(null, ClaimFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
        }

        [Fact]
        public void CreateUserClaim_ShouldReturnError_ClaimIsInvalid()
        {
            var response = UserClaim.Create(UserFake(), null);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
        }
    }
}
