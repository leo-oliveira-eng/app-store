using Catalog.Domain.Authors.Commands;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Messages.Core.Extensions;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Application.Tests.AuthorTests
{
    public class DeleteAuthorUnitTests : AuthorAppServiceTests
    {
        [Fact]
        public async Task DeleteAsync_Success()
        {
            _mediator.Setup(x => x.SendCommand<DeleteAuthorCommand, Response>(It.IsAny<DeleteAuthorCommand>()))
                .ReturnsAsync(Response.Create())
                .Verifiable();

            var response = await AuthorApplicationService.DeleteAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            _mediator.Verify();
        }

        [Fact]
        public async Task DeleteAsync_CommandFailed()
        {
            _mediator.Setup(x => x.SendCommand<DeleteAuthorCommand, Response>(It.IsAny<DeleteAuthorCommand>()))
                .ReturnsAsync(Response.Create().WithBusinessError("Any error"))
                .Verifiable();

            var response = await AuthorApplicationService.DeleteAsync(Guid.NewGuid());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _mediator.Verify();
        }
    }
}
