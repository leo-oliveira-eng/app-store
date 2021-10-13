using BaseEntity.Domain.Mediator.Contracts;
using Catalog.Application.Apps.Contracts;
using Catalog.Application.Apps.Services;
using Catalog.Application.Tests.Shared;
using Moq;

namespace Catalog.Application.Tests.AppTests
{
    public class AppApplicationServiceTests : BaseMock
    {
        protected readonly Mock<IMediatorHandler> _mediator = new Mock<IMediatorHandler>();

        protected IAppApplicationService AppAplicationService { get; set; }

        public AppApplicationServiceTests()
        {
            AppAplicationService = new AppApplicationService(_mediator.Object);
        }
    }
}
