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
    public class UpdateAuthorUnitTests : BaseMock
    {
        private readonly Mock<IAuthorRepository> _authorRepository = new Mock<IAuthorRepository>();

        protected readonly Mock<ReplaceOneResult> _updateResult = new Mock<ReplaceOneResult>();

        public UpdateAuthorCommandHandler CommandHandler { get; set; }

        public UpdateAuthorUnitTests()
        {
            CommandHandler = new UpdateAuthorCommandHandler(_authorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewAuthor_ValidParameters()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(AuthorFake())
                .Verifiable();

            _updateResult.Setup(x => x.IsAcknowledged).Returns(true);

            _authorRepository.Setup(x => x.UpdateAsync(It.IsAny<Author>()))
                .ReturnsAsync(_updateResult.Object)
                .Verifiable();

            var response = await CommandHandler.Handle(UpdateAuthorCommandFake(), default);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Author));
            response.Data.Value.DomainEvents.Should().NotBeEmpty();
            _authorRepository.Verify();
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_EmailIsInvalid()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));
            _authorRepository.Setup(x => x.UpdateAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(UpdateAuthorCommandFake(email: "nothing"), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("Email"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _authorRepository.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_PhoneNumberIsInvalid()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));
            _authorRepository.Setup(x => x.UpdateAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(UpdateAuthorCommandFake(phoneNumber: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("PhoneNumber"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _authorRepository.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_WebSiteIsInvalid()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));
            _authorRepository.Setup(x => x.UpdateAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(UpdateAuthorCommandFake(webSite: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("WebSite"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _authorRepository.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_BrandLogoIsInvalid()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));
            _authorRepository.Setup(x => x.UpdateAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(UpdateAuthorCommandFake(brandLogo: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("BrandLogo"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _authorRepository.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_AuthorNotFound()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<Author>.Create()).Verifiable();
            _authorRepository.Setup(x => x.UpdateAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(UpdateAuthorCommandFake(), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify();
            _authorRepository.Verify(x => x.UpdateAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_UpdateAsyncFailed()
        {
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()))
                .ReturnsAsync(AuthorFake())
                .Verifiable();

            _updateResult.Setup(x => x.IsAcknowledged).Returns(false);

            _authorRepository.Setup(x => x.UpdateAsync(It.IsAny<Author>()))
                .ReturnsAsync(_updateResult.Object)
                .Verifiable();

            var response = await CommandHandler.Handle(UpdateAuthorCommandFake(), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.CriticalError));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify();
        }
    }
}
