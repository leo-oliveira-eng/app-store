using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Models;
using Catalog.Messages.Responses;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Application.Tests.AuthorTests
{
    public class CreateAsyncUnitTests : AuthorAppServiceTests
    {
        [Fact]
        public async Task CreateAsync_Success_ValidParameters()
        {
            _mediator.Setup(x => x.SendCommand<CreateAuthorCommand, Response<Author>>(It.IsAny<CreateAuthorCommand>()))
                .ReturnsAsync(Response<Author>.Create(AuthorFake()))
                .Verifiable();

            var response = await AuthorApplicationService.CreateAsync(CreateAuthorRequestMessageFake());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(CreateAuthorResponseMessage));
            _mediator.Verify();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_RequestMessageIsNull()
        {
            _mediator.Setup(x => x.SendCommand<CreateAuthorCommand, Response<Author>>(It.IsAny<CreateAuthorCommand>()));

            var response = await AuthorApplicationService.CreateAsync(null);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediator.Verify(x => x.SendCommand<CreateAuthorCommand, Response<Author>>(It.IsAny<CreateAuthorCommand>()), Times.Never);
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnBusinessError_CommandFailed()
        {
            _mediator.Setup(x => x.SendCommand<CreateAuthorCommand, Response<Author>>(It.IsAny<CreateAuthorCommand>()))
                .ReturnsAsync(Response<Author>.Create().WithBusinessError("Any error"))
                .Verifiable();

            var response = await AuthorApplicationService.CreateAsync(CreateAuthorRequestMessageFake());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediator.Verify();
        }
    }
}
