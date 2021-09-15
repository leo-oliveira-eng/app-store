using FluentAssertions;
using IdentityServer.Domain.Models;
using Messages.Core.Enums;
using Xunit;

namespace IdentityServer.Domain.Tests.UserTests
{
    public class CreateUnitTests
    {
        [Fact]
        public void CreateUser_ShouldReturnsSuccessWithValidParams()
        {
            var response = User.Create("783.297.220-31", "Any Name", "Abc123", "any@nothing.com");

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
        }

        [Fact]
        public void CreateUser_CpfIsInvalid_ShouldReturnBusinessError()
        {
            var response = User.Create(string.Empty, "Any Name", "Abc123", "any@nothing.com");

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("cpf"));
            response.Data.HasValue.Should().BeFalse();
        }

        [Fact]
        public void CreateUser_NameIsEmpty_ShouldReturnBusinessError()
        {
            var response = User.Create("783.297.220-31", string.Empty, "Abc123", "any@nothing.com");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("name"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        [InlineData("123456")]
        [InlineData("Abc00")]
        [InlineData("asdf")]
        [InlineData("ABC123")]
        [InlineData("asdflk")]
        [InlineData("!@#$%&")]
        public void CreateUser_PasswordIsInvalid_ShouldReturnBusinessError(string password)
        {
            var response = User.Create("783.297.220-31", "Any Name", password, "any@nothing.com");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("password"));
        }

        [Fact]
        public void CreateUser_EmailIsInvalid_ShouldReturnBusinessError()
        {
            var response = User.Create("783.297.220-31", "Any Name", "Abc123", "anything.com.br");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("email"));
        }
    }
}
