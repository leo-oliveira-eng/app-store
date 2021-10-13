using Catalog.Domain.Apps.Commands;
using Catalog.Domain.Apps.Models;
using Catalog.Messages.Responses;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Application.Tests.AppTests
{
    public class CreateAsyncUnitTests : AppApplicationServiceTests
    {
        [Fact]
        public async Task CreateAsync_Success_ValidParameters()
        {
            _mediator.Setup(x => x.SendCommand<CreateAppCommand, Response<App>>(It.IsAny<CreateAppCommand>()))
                .ReturnsAsync(Response<App>.Create(AppFake()))
                .Verifiable();

            var response = await AppAplicationService.CreateAsync(CreateAppRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(AppResponseMessage));
            _mediator.Verify();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_RequestMessageIsNull()
        {
            _mediator.Setup(x => x.SendCommand<CreateAppCommand, Response<App>>(It.IsAny<CreateAppCommand>()));

            var response = await AppAplicationService.CreateAsync(null);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediator.Verify(x => x.SendCommand<CreateAppCommand, Response<App>>(It.IsAny<CreateAppCommand>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_CommandFailed()
        {
            _mediator.Setup(x => x.SendCommand<CreateAppCommand, Response<App>>(It.IsAny<CreateAppCommand>()))
                .ReturnsAsync(Response<App>.Create().WithBusinessError("Any error"))
                .Verifiable();

            var response = await AppAplicationService.CreateAsync(CreateAppRequestMessageFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediator.Verify();
        }
    }
}
