using IdentityServer.Application.Services;
using IdentityServer.Application.Tests.Shared;
using IdentityServer.Domain.Services.Contracts;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace IdentityServer.Application.Tests.UserAppServiceTests
{
    public class UserAppServiceUnitTests : BaseMock
    {
        protected readonly Mock<IUserService> _userService = new Mock<IUserService>();

        protected readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        protected UserApplicationService UserApplicationService { get; set; }

        public UserAppServiceUnitTests()
        {
            var scope = MapsterConfig.CreateScope();

            UserApplicationService = new UserApplicationService(_userService.Object, scope.ServiceProvider.GetService<IMapper>());
        }
    }
}
