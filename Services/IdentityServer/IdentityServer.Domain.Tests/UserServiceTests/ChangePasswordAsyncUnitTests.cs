using FizzWare.NBuilder;
using FluentAssertions;
using IdentityServer.Domain.Events;
using IdentityServer.Domain.Models;
using IdentityServer4.Models;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.Domain.Tests.UserServiceTests
{
    public class ChangePasswordAsyncUnitTests : UserServiceUnitTests
    {
        private User User { get; }

        private readonly Guid _recoverPasswordCode = Guid.NewGuid();

        public ChangePasswordAsyncUnitTests()
        {
            User = Builder<User>.CreateNew()
                .With(_ => _.PasswordRecoverCode, _recoverPasswordCode)
                .With(_ => _.Password, "secret")
                .With(_ => _.RecoverPasswordExpirationDate, DateTime.Now.AddHours(2))
                .Build();
        }

        [Fact]
        public async Task ChangePassword_Success_ValidParameters()
        {
            var newPassword = "Abcd1234";
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(true).Verifiable();
            _userRepository.Setup(_ => _.FindByPasswordRecoverCode(It.IsAny<Guid>())).ReturnsAsync(User).Verifiable();
            _domainEventHandler.Setup(_ => _.Raise(It.IsAny<PasswordChangedEvent>())).Verifiable();

            var response = await UserService.ChangePasswordAsync(ChangePasswordDtoFake(_recoverPasswordCode, newPassword, newPassword));

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            User.PasswordRecoverCode.Should().BeNull();
            User.RecoverPasswordExpirationDate.Should().BeNull();
            User.Password.Should().Be(newPassword.Sha256());
            _userRepository.Verify();
            _uow.Verify();
            _domainEventHandler.Verify();
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnBusinessError_UserNotFound()
        {
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(true);
            _userRepository.Setup(_ => _.FindByPasswordRecoverCode(It.IsAny<Guid>())).ReturnsAsync(Maybe<User>.Create()).Verifiable();
            _domainEventHandler.Setup(_ => _.Raise(It.IsAny<PasswordChangedEvent>()));

            var response = await UserService.ChangePasswordAsync(ChangePasswordDtoFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _userRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
            _domainEventHandler.Verify(_ => _.Raise(It.IsAny<RecoverPasswordEvent>()), Times.Never);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnBusinessError_RecoverCodeIsInvalid()
        {
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(true);
            _userRepository.Setup(_ => _.FindByPasswordRecoverCode(It.IsAny<Guid>())).ReturnsAsync(User).Verifiable();
            _domainEventHandler.Setup(_ => _.Raise(It.IsAny<PasswordChangedEvent>()));

            var response = await UserService.ChangePasswordAsync(ChangePasswordDtoFake(passwordRecoverCode: Guid.NewGuid()));

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _userRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
            _domainEventHandler.Verify(_ => _.Raise(It.IsAny<RecoverPasswordEvent>()), Times.Never);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnBusinessError_CommitAsyncFailed()
        {
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(false);
            _userRepository.Setup(_ => _.FindByPasswordRecoverCode(It.IsAny<Guid>())).ReturnsAsync(User).Verifiable();
            _domainEventHandler.Setup(_ => _.Raise(It.IsAny<PasswordChangedEvent>()));

            var response = await UserService.ChangePasswordAsync(ChangePasswordDtoFake(passwordRecoverCode: _recoverPasswordCode));

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.CriticalError));
            _userRepository.Verify();
            _uow.Verify();
            _domainEventHandler.Verify(_ => _.Raise(It.IsAny<RecoverPasswordEvent>()), Times.Never);
        }
    }
}
