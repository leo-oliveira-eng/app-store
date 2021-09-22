using BaseEntity.Domain.UnitOfWork;
using FluentAssertions;
using IdentityServer.Domain.Events;
using IdentityServer.Domain.Events.Contracts;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Repositories;
using IdentityServer.Domain.Services;
using IdentityServer.Domain.Tests.Shared;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.Domain.Tests.UserServiceTests
{
    public class RecoverPasswordAsyncUnitTests : BaseMock
    {
        protected readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

        protected readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();

        protected readonly Mock<IDomainEventHandler> _domainEventHandler = new Mock<IDomainEventHandler>();

        protected RecoverPasswordService UserService { get; set; }

        public RecoverPasswordAsyncUnitTests()
        {
            UserService = new RecoverPasswordService(_userRepository.Object, _uow.Object, _domainEventHandler.Object);
        }

        [Fact]
        public async Task RecoverPassword_Success_ValidParameters()
        {
            var user = UserFake();
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(true).Verifiable();
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(user).Verifiable();
            _domainEventHandler.Setup(_ => _.Raise(It.IsAny<RecoverPasswordEvent>())).Verifiable();

            var response = await UserService.RecoverPasswordAsync("987.654.321-00");

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            user.PasswordRecoverCode.Should().NotBeNull();
            user.RecoverPasswordExpirationDate.Should().NotBeNull();
            _userRepository.Verify();
            _uow.Verify();
            _domainEventHandler.Verify();
        }

        [Fact]
        public async Task RecoverPassword_ShouldReturnBusinessError_UserNotFound()
        {
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(true);
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(Maybe<User>.Create()).Verifiable();
            _domainEventHandler.Setup(_ => _.Raise(It.IsAny<RecoverPasswordEvent>()));

            var response = await UserService.RecoverPasswordAsync("987.654.321-00");

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _userRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
            _domainEventHandler.Verify(_ => _.Raise(It.IsAny<RecoverPasswordEvent>()), Times.Never);
        }

        [Fact]
        public async Task RecoverPassword_ShouldReturnBusinessError_CommitAsyncFailed()
        {
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(false).Verifiable();
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(UserFake()).Verifiable();
            _domainEventHandler.Setup(_ => _.Raise(It.IsAny<RecoverPasswordEvent>()));

            var response = await UserService.RecoverPasswordAsync("987.654.321-00");

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.CriticalError));
            _userRepository.Verify();
            _uow.Verify();
            _domainEventHandler.Verify(_ => _.Raise(It.IsAny<RecoverPasswordEvent>()), Times.Never);
        }
    }
}
