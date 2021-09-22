using FluentAssertions;
using IdentityServer.Domain.Services.Dtos;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.Application.Tests.UserAppServiceTests
{
    public class ChangePasswordAsyncUnitTests : UserAppServiceUnitTests
    {
        [Fact]
        public async Task ChangePassword_Success_ValidParameters()
        {
            _changePasswordService.Setup(_ => _.ChangePasswordAsync(It.IsAny<ChangePasswordDto>()))
                .ReturnsAsync(Response.Create())
                .Verifiable();

            var response = await UserApplicationService.ChangePasswordAsync(ChangePasswordRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            _changePasswordService.Verify();
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnBusinessError_RequestMessageIsNull()
        {
            _changePasswordService.Setup(_ => _.ChangePasswordAsync(It.IsAny<ChangePasswordDto>()));

            var response = await UserApplicationService.ChangePasswordAsync(null);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _changePasswordService.Verify(_ => _.ChangePasswordAsync(It.IsAny<ChangePasswordDto>()), Times.Never);
        }

        [Fact]
        public async Task ChangePassword_ShouldReturnBusinessError_ChangePasswordServiceReturnsError()
        {
            _changePasswordService.Setup(_ => _.ChangePasswordAsync(It.IsAny<ChangePasswordDto>()))
                .ReturnsAsync(Response.Create().WithBusinessError("Any error message"))
                .Verifiable();

            var response = await UserApplicationService.ChangePasswordAsync(ChangePasswordRequestMessageFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _changePasswordService.Verify();
        }
    }
}
