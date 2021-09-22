using Catalog.Domain.Authors.Handlers;
using Catalog.Domain.Authors.Models;
using Catalog.Domain.Authors.Repositories;
using Catalog.Domain.Tests.Shared;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using MongoDB.Driver;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Domain.Tests.AuthorTests
{
    public class DeleteAuthorUnitTests : BaseMock
    {
        private readonly Mock<IAuthorRepository> _authorRepository = new Mock<IAuthorRepository>();

        protected readonly Mock<UpdateResult> _updateResult = new Mock<UpdateResult>();

        private DeleteAuthorCommandHandler Handler { get; }

        public DeleteAuthorUnitTests()
        {
            Handler = new DeleteAuthorCommandHandler(_authorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldDeleteAuthor_Success()
        {
            _updateResult.Setup(x => x.IsAcknowledged).Returns(true);
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(AuthorFake()).Verifiable();
            _authorRepository.Setup(x => x.RemoveAsync(It.IsAny<Author>()))
                .ReturnsAsync(_updateResult.Object)
                .Verifiable();

            var response = await Handler.Handle(DeleteAuthorCommandFake(), default);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()));
            _authorRepository.Verify(x => x.RemoveAsync(It.IsAny<Author>()));
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_AuthorNotFound()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(Maybe<Author>.Create())
                .Verifiable();

            _authorRepository.Setup(x => x.RemoveAsync(It.IsAny<Author>()));

            var response = await Handler.Handle(DeleteAuthorCommandFake(Guid.NewGuid()), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _authorRepository.Verify();
            _authorRepository.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_GuidIsEmpty()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            _authorRepository.Setup(x => x.RemoveAsync(It.IsAny<Author>()));

            var response = await Handler.Handle(DeleteAuthorCommandFake(Guid.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _authorRepository.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_RepositoryFailed()
        {
            _updateResult.Setup(x => x.IsAcknowledged).Returns(false);
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(AuthorFake()).Verifiable();
            _authorRepository.Setup(x => x.RemoveAsync(It.IsAny<Author>()))
                .ReturnsAsync(_updateResult.Object)
                .Verifiable();

            var response = await Handler.Handle(DeleteAuthorCommandFake(Guid.NewGuid()), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.CriticalError));
            _authorRepository.Verify();
            _authorRepository.Verify();
        }
    }
}
