using FluentAssertions;
using IdentityServer.Domain.Models;
using IdentityServer.Domain.Services.Dtos;
using IdentityServer.Messaging.Responses;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.Application.Tests.UserAppServiceTests
{
    public class CreateAsyncUnitTests : UserAppServiceUnitTests
    {
        [Fact]
        public async Task CreateAsync_Success_ValidParametes()
        {
            _userService.Setup(_ => _.CreateAsync(It.IsAny<CreateUserDto>())).ReturnsAsync(UserFake());

            var response = await UserApplicationService.CreateAsync(CreateUserRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CreateUserResponseMessage));
            _userService.Verify();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_DtoIsNull()
        {
            _userService.Setup(_ => _.CreateAsync(It.IsAny<CreateUserDto>()));

            var response = await UserApplicationService.CreateAsync(null);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _userService.Verify(_ => _.CreateAsync(It.IsAny<CreateUserDto>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_UserServiceReturnError()
        {
            var error = Response<User>.Create().WithBusinessError("Any error");
            _userService.Setup(_ => _.CreateAsync(It.IsAny<CreateUserDto>())).ReturnsAsync(error);

            var response = await UserApplicationService.CreateAsync(CreateUserRequestMessageFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _userService.Verify();
        }
    }
}
