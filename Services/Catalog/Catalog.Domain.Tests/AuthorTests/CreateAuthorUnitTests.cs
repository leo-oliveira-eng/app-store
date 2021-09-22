using Catalog.Domain.Authors.Handlers;
using Catalog.Domain.Authors.Models;
using Catalog.Domain.Authors.Repositories;
using Catalog.Domain.Tests.Shared;
using FluentAssertions;
using Messages.Core.Enums;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Catalog.Domain.Tests.AuthorTests
{
    public class CreateAuthorUnitTests : BaseMock
    {
        private readonly Mock<IAuthorRepository> _authorRepository = new Mock<IAuthorRepository>();

        public CreateAuthorCommandHandler CommandHandler { get; set; }

        public CreateAuthorUnitTests()
        {
            CommandHandler = new CreateAuthorCommandHandler(_authorRepository.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateNewAuthor_ValidParameters()
        {
            _authorRepository.Setup(x => x.AddAsync(It.IsAny<Author>())).Verifiable();

            var response = await CommandHandler.Handle(CreateAuthorCommandFake(), default);

            response.Should().NotBeNull();
            response.HasError.Should().BeFalse();
            response.Messages.Should().BeEmpty();
            response.Data.HasValue.Should().BeTrue();
            response.Data.Value.Should().BeOfType(typeof(Author));
            response.Data.Value.DomainEvents.Should().NotBeEmpty();
            _authorRepository.Verify();
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_NameIsInvalid()
        {
            _authorRepository.Setup(x => x.AddAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(CreateAuthorCommandFake(name: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("Name"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.AddAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_CNPJIsInvalid()
        {
            _authorRepository.Setup(x => x.AddAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(CreateAuthorCommandFake(cnpj: "12345"), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("CNPJ"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.AddAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_EmailIsInvalid()
        {
            _authorRepository.Setup(x => x.AddAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(CreateAuthorCommandFake(email: "www.nothing.com"), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("Email"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.AddAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_PhoneNumberIsInvalid()
        {
            _authorRepository.Setup(x => x.AddAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(CreateAuthorCommandFake(phoneNumber: string.Empty), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("PhoneNumber"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.AddAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_WebSiteIsInvalid()
        {
            _authorRepository.Setup(x => x.AddAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(CreateAuthorCommandFake(webSite: "nothing@noth.com"), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("WebSite"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.AddAsync(It.IsAny<Author>()), Times.Never);
        }

        [Fact]
        public async Task Handle_ShouldReturnBusinessError_BrandLogoIsInvalid()
        {
            _authorRepository.Setup(x => x.AddAsync(It.IsAny<Author>()));

            var response = await CommandHandler.Handle(CreateAuthorCommandFake(brandLogo: "nothing@noth.com"), default);

            response.HasError.Should().BeTrue();
            response.Messages.Count.Should().BeGreaterThan(0);
            response.Messages.Should().Contain(x => x.Type.Equals(MessageType.BusinessError));
            response.Messages.Should().Contain(x => x.Property.Equals("BrandLogo"));
            response.Data.HasValue.Should().BeFalse();
            _authorRepository.Verify(x => x.AddAsync(It.IsAny<Author>()), Times.Never);
        }
    }
}
