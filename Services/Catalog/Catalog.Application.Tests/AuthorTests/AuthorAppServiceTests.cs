using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Application.Authors.Services;
using Catalog.Application.Tests.Shared;
using Moq;

namespace Catalog.Application.Tests.AuthorTests
{
    public class AuthorAppServiceTests : BaseMock
    {
        protected readonly Mock<IMediatorHandler> _mediator = new Mock<IMediatorHandler>();

        protected AuthorApplicationService AuthorApplicationService { get; set; }

        public AuthorAppServiceTests()
        {
            AuthorApplicationService = new AuthorApplicationService(_mediator.Object);
        }
    }
}
