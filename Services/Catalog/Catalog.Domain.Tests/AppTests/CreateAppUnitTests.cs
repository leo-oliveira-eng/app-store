using Catalog.Domain.Apps.Commands;
using Catalog.Domain.Apps.Handlers;
using Catalog.Domain.Apps.Models;
using Catalog.Domain.Apps.Repositories;
using Catalog.Domain.Authors.Models;
using Catalog.Domain.Authors.Repositories;
using Catalog.Domain.Tests.Shared;
using FluentAssertions;
using Messages.Core;
using Messages.Core.Enums;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Domain.Tests.AppTests
{
    public class CreateAppUnitTests : BaseMock
    {
        private readonly Mock<IAppRepository> _appRepository = new Mock<IAppRepository>();

        private readonly Mock<IAuthorRepository> _authorRepository = new Mock<IAuthorRepository>();

        private CreateAppCommandHandler CommandHandler { get; }

        public CreateAppUnitTests()
        {
            CommandHandler = new CreateAppCommandHandler(_appRepository.Object, _authorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewApp_ValidParameters()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>())).Verifiable();
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(AuthorFake()).Verifiable();

            var response = await CommandHandler.Handle(CreateAppCommandFake(), default);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(App));
            _authorRepository.Verify();
            _appRepository.Verify();
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_NameIsEmpty()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            var response = await CommandHandler.Handle(CreateAppCommandFake(name: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("Name"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_TitleIsEmpty()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            var response = await CommandHandler.Handle(CreateAppCommandFake(title: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals(nameof(App.Title)));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_ReleaseDateIsInvalid()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            var response = await CommandHandler.Handle(CreateAppCommandFake(releaseDate: (DateTime)default), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals(nameof(App.ReleaseDate)));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_VersionIsInvalid()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            var response = await CommandHandler.Handle(CreateAppCommandFake(version: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals(nameof(App.Version)));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_SizeIsInvalid()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            var response = await CommandHandler.Handle(CreateAppCommandFake(size: (int)default), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals(nameof(App.Size)));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_PriceIsInvalid()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            var response = await CommandHandler.Handle(CreateAppCommandFake(price: (decimal)default), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals(nameof(App.Price)));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_AuthorIdIsInvalid()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>()));

            var response = await CommandHandler.Handle(CreateAppCommandFake(authorId: Guid.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals(nameof(CreateAppCommand.AuthorID)));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.FindAsync(It.IsAny<Guid>()), Times.Never);
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnResponseWithBusinessError_AuthorNotFound()
        {
            _appRepository.Setup(x => x.AddAsync(It.IsAny<App>()));
            _authorRepository.Setup(x => x.FindAsync(It.IsAny<Guid>())).ReturnsAsync(Maybe<Author>.Create()).Verifiable();

            var response = await CommandHandler.Handle(CreateAppCommandFake(), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Data.HasValue.Should().BeFalse();
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals(nameof(App.Author)));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify();
            _appRepository.Verify(x => x.AddAsync(It.IsAny<App>()), Times.Never);
        }
    }
}
