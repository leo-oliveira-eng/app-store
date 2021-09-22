using FluentAssertions;
using IdentityServer.Domain.Models;
using Messages.Core.Enums;
using Xunit;

namespace IdentityServer.Domain.Tests.ClaimTests
{
    public class CreateUnitTests
    {
        [Fact]
        public void CreateClaim_ShouldReturnSuccessWithValidParams()
        {
            var response = Claim.Create("any type", "any value");

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Claim));
        }

        [Fact]
        public void CreateClaim_ShouldReturnError_TypeIsInvalid()
        {
            var response = Claim.Create(string.Empty, "any value");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
        }

        [Fact]
        public void CreateClaim_ShouldReturnError_ValueIsInvalid()
        {
            var response = Claim.Create("any type", string.Empty);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(message => message.Type.Equals(MessageType.BusinessError));
        }
    }
}
