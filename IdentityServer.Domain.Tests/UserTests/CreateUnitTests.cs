using FluentAssertions;
using IdentityServer.Domain.Enums;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Tests.Shared;
using Messages.Core.Enums;
using System;
using Xunit;

namespace IdentityServer.Domain.Tests.UserTests
{
    public class CreateUnitTests : BaseMock
    {
        [Fact]
        public void CreateUser_ShouldReturnsSuccessWithValidParams()
        {
            var response = User.Create(CreateUserDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
        }

        [Fact]
        public void CreateUser_CpfIsInvalid_ShouldReturnBusinessError()
        {
            var response = User.Create(CreateUserDtoFake(cpf: string.Empty));

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("Cpf"));
            response.Data.HasValue.Should().BeFalse();
        }

        [Fact]
        public void CreateUser_NameIsEmpty_ShouldReturnBusinessError()
        {
            var response = User.Create(CreateUserDtoFake(name: string.Empty));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("Name"));
        }

        [Theory]
        [InlineData("")]
        [InlineData("123456")]
        [InlineData("Abc00")]
        [InlineData("asdf")]
        [InlineData("ABC123")]
        [InlineData("asdflk")]
        [InlineData("!@#$%&")]
        public void CreateUser_PasswordIsInvalid_ShouldReturnBusinessError(string password)
        {
            var response = User.Create(CreateUserDtoFake(password: password));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("Password"));
        }

        [Fact]
        public void CreateUser_EmailIsInvalid_ShouldReturnBusinessError()
        {
            var response = User.Create(CreateUserDtoFake(email: "anything.com.br"));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("Email"));
        }

        [Fact]
        public void CreateUser_BirthDateIsInvalid_ShouldReturnBusinessError()
        {
            var response = User.Create(CreateUserDtoFake(birthDate: (DateTime)default));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("BirthDate"));
        }

        [Fact]
        public void CreateUser_GenderIsInvalid_ShouldReturnBusinessError()
        {
            var response = User.Create(CreateUserDtoFake(gender: (GenderType)10));

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("Gender"));
        }

        [Fact]
        public void CreateUser_AddressIsInvalid_ShouldReturnBusinessError()
        {
            var dto = CreateUserDtoFake();
            dto.Address = null;

            var response = User.Create(dto);

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Data.HasValue.Should().BeFalse();
            response.Data.Value.Should().BeNull();
            response.Messages.Should().Contain(x => x.Property.Equals("Address"));
        }
    }
}
