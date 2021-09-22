using FizzWare.NBuilder;
using FluentAssertions;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Tests.Shared;
using IdentityServer4.Models;
using Messages.Core.Enums;
using System;
using Xunit;

namespace IdentityServer.Domain.Tests.UserTests
{
    public class ChangePasswordUnitTests : BaseMock
    {
        [Fact]
        public void ChangePasswordUser_ShouldReturnSuccess_ValidPasswordAndRecoverCode()
        {
            var passwordRecoverCode = Guid.NewGuid();
            var user = Builder<User>.CreateNew()
                .With(x => x.PasswordRecoverCode, passwordRecoverCode)
                .With(x => x.RecoverPasswordExpirationDate, DateTime.Now.AddHours(2))
                .Build();

            var response = user.ChangePassword(passwordRecoverCode, "Abc123", "Abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            user.Password.Should().Be("Abc123".Sha256());
        }

        [Fact]
        public void ChangePasswordUser_ShouldReturnBusineessError_InvalidPasswordRecoverCode()
        {
            var user = Builder<User>.CreateNew()
                .With(x => x.PasswordRecoverCode, Guid.NewGuid())
                .With(x => x.RecoverPasswordExpirationDate, DateTime.Now)
                .Build();

            var response = user.ChangePassword(Guid.Empty, "Abc123", "Abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
        }

        [Fact]
        public void ChangePasswordUser_ShouldReturnBusineessError_CodeHasExpired()
        {
            var passwordRecoverCode = Guid.NewGuid();
            var user = Builder<User>.CreateNew()
                .With(x => x.PasswordRecoverCode, passwordRecoverCode)
                .With(x => x.RecoverPasswordExpirationDate, DateTime.Now.AddDays(-1))
                .Build();

            var response = user.ChangePassword(passwordRecoverCode, "Abc123", "Abc123");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
        }

        [Fact]
        public void ChangePasswordUser_ShouldReturnBusineessError_InvalidPassword()
        {
            var passwordRecoverCode = Guid.NewGuid();
            var user = Builder<User>.CreateNew()
                .With(x => x.PasswordRecoverCode, passwordRecoverCode)
                .With(x => x.RecoverPasswordExpirationDate, DateTime.Now.AddHours(2))
                .Build();

            var response = user.ChangePassword(passwordRecoverCode, "none", "none");

            response.Should().NotBeNull();
            response.HasError.Should().BeTrue();
            response.Messages.Should().HaveCount(1);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
        }
    }
}
