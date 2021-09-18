using BaseEntity.Domain.UnitOfWork;
using FluentAssertions;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Repositories;
using IdentityServer.Domain.Services;
using IdentityServer.Domain.Services.Dtos;
using IdentityServer.Domain.Tests.Shared;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.Domain.Tests.UserServiceTests
{
    public class CreateAsyncUnitTests : BaseMock
    {
        protected readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

        protected readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();

        protected CreateUserService UserService { get; set; }

        public CreateAsyncUnitTests()
        {
            UserService = new CreateUserService(_userRepository.Object, _uow.Object);
        }

        [Fact]
        public async Task CreateAsync_Success_ValidParametes()
        {
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(true).Verifiable();
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(Maybe<User>.Create()).Verifiable();

            var response = await UserService.CreateAsync(CreateUserDtoFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(User));
            _userRepository.Verify();
            _uow.Verify();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_DtoIsNull()
        {
            _uow.Setup(_ => _.CommitAsync());
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>()));

            var response = await UserService.CreateAsync(null);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _userRepository.Verify(_ => _.FindByCPFAsync(It.IsAny<string>()), Times.Never);
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_UserAlreadyRegistered()
        {
            _uow.Setup(_ => _.CommitAsync());
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(UserFake()).Verifiable();

            var response = await UserService.CreateAsync(CreateUserDtoFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _userRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_UserIsInvalid()
        {
            _uow.Setup(_ => _.CommitAsync());
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(Maybe<User>.Create()).Verifiable();

            var response = await UserService.CreateAsync(CreateUserDtoFake(cpf: string.Empty));

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _userRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_InvalidClaim()
        {
            _uow.Setup(_ => _.CommitAsync());
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(Maybe<User>.Create()).Verifiable();
            var dto = CreateUserDtoFake();
            dto.Claims.Add(new ClaimDto());

            var response = await UserService.CreateAsync(dto);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _userRepository.Verify();
            _uow.Verify(_ => _.CommitAsync(), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_CommitAsyncFailed()
        {
            _uow.Setup(_ => _.CommitAsync()).ReturnsAsync(false).Verifiable();
            _userRepository.Setup(_ => _.FindByCPFAsync(It.IsAny<string>())).ReturnsAsync(Maybe<User>.Create()).Verifiable();

            var response = await UserService.CreateAsync(CreateUserDtoFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.CriticalError));
            response.Data.HasValue.Should().BeFalse();
            _userRepository.Verify();
            _uow.Verify();
        }
    }
}
