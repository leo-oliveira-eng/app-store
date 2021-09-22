using Catalog.Domain.Authors.Models;
using Catalog.Messages.Responses;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Application.Tests.AuthorTests
{
    public class FindAsyncUnitTests : AuthorAppServiceTests
    {
        [Fact]
        public async Task FindAuthor_ExistingUser()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(AuthorFake()).Verifiable();

            var response = await AuthorApplicationService.FindAsync(Guid.NewGuid());

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(AuthorResponseMessage));
            _authorRepository.Verify();
        }

        [Fact]
        public async Task FindAuthor_AuthorNotFound()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Author>.Create())
                .Verifiable();

            var response = await AuthorApplicationService.FindAsync(Guid.NewGuid());

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify();
        }
    }
}
