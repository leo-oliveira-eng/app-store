using Catalog.Domain.Authors.Commands;
using Catalog.Domain.Authors.Models;
using Catalog.Messages.Responses;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Application.Tests.AuthorTests
{
    public class UpdateAsyncUnitTests : AuthorAppServiceTests
    {
        [Fact]
        public async Task UpdateAsync_Success_ValidParameters()
        {
            _mediator.Setup(x => x.SendCommand<UpdateAuthorCommand, Response<Author>>(It.IsAny<UpdateAuthorCommand>()))
                .ReturnsAsync(Response<Author>.Create(AuthorFake()))
                .Verifiable();

            var response = await AuthorApplicationService.UpdateAsync(UpdateAuthorRequestMessageFake() ,Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(AuthorResponseMessage));
            _mediator.Verify();
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnBusinessError_RequestMessageIsNull()
        {
            _mediator.Setup(x => x.SendCommand<UpdateAuthorCommand, Response<Author>>(It.IsAny<UpdateAuthorCommand>()));

            var response = await AuthorApplicationService.UpdateAsync(null, Guid.NewGuid());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediator.Verify(x => x.SendCommand<UpdateAuthorCommand, Response<Author>>(It.IsAny<UpdateAuthorCommand>()), Times.Never);
        }

        [Fact]
        public async Task UpdateAsync_ShouldReturnBusinessError_CommandFailed()
        {
            _mediator.Setup(x => x.SendCommand<UpdateAuthorCommand, Response<Author>>(It.IsAny<UpdateAuthorCommand>()))
                .ReturnsAsync(Response<Author>.Create().WithBusinessError("Any error"))
                .Verifiable();

            var response = await AuthorApplicationService.UpdateAsync(UpdateAuthorRequestMessageFake(), Guid.NewGuid());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _mediator.Verify();
        }
    }
}
