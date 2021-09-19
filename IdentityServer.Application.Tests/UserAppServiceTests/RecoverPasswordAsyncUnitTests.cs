using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace IdentityServer.Application.Tests.UserAppServiceTests
{
    public class RecoverPasswordAsyncUnitTests : UserAppServiceUnitTests
    {
        [Fact]
        public async Task RecoverPassword_Success_ValidParameters()
        {
            _recoverPasswordService.Setup(_ => _.RecoverPasswordAsync(It.IsAny<string>()))
                .ReturnsAsync(Response.Create())
                .Verifiable();

            var response = await UserApplicationService.RecoverPasswordAsync(RecoverPasswordRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            _recoverPasswordService.Verify();
        }

        [Fact]
        public async Task RecoverPassword_ShouldReturnBusinessError_RequestMessageIsNull()
        {
            _recoverPasswordService.Setup(_ => _.RecoverPasswordAsync(It.IsAny<string>()));

            var response = await UserApplicationService.RecoverPasswordAsync(null);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _recoverPasswordService.Verify(_ => _.RecoverPasswordAsync(It.IsAny<string>()), Times.Never);
        }

        [Fact]
        public async Task RecoverPassword_ShouldReturnBusinessError_RecoverPasswordServiceReturnsError()
        {
            _recoverPasswordService.Setup(_ => _.RecoverPasswordAsync(It.IsAny<string>()))
                .ReturnsAsync(Response.Create().WithBusinessError("Any error message"))
                .Verifiable();

            var response = await UserApplicationService.RecoverPasswordAsync(RecoverPasswordRequestMessageFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _recoverPasswordService.Verify();
        }
    }
}
