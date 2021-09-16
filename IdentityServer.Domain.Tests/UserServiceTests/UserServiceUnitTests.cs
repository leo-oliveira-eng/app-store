using BaseEntity.Domain.UnitOfWork;
using IdentityServer.Domain.Events.Contracts;
using IdentityServer.Domain.Repositories;
using IdentityServer.Domain.Services;
using IdentityServer.Domain.Tests.Shared;
using Moq;

namespace IdentityServer.Domain.Tests.UserServiceTests
{
    public class UserServiceUnitTests : BaseMock
    {
        #region Fields

        protected readonly Mock<IUnitOfWork> _uow = new Mock<IUnitOfWork>();

        protected readonly Mock<IUserRepository> _userRepository = new Mock<IUserRepository>();

        protected readonly Mock<IDomainEventHandler> _domainEventHandler = new Mock<IDomainEventHandler>();

        #endregion

        #region Properties

        protected UserService UserService { get; set; }

        #endregion

        #region Constructors

        public UserServiceUnitTests()
        {
            UserService = new UserService(_userRepository.Object, _uow.Object, _domainEventHandler.Object);
        }

        #endregion
    }
}
