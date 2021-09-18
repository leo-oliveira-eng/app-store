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
        protected readonly Mock<ICreateUserService> _userService = new Mock<ICreateUserService>();

        protected readonly Mock<IMapper> _mapper = new Mock<IMapper>();

        protected readonly Mock<IRecoverPasswordService> _recoverPasswordService = new Mock<IRecoverPasswordService>();

        protected UserApplicationService UserApplicationService { get; set; }

        public UserAppServiceUnitTests()
        {
            var scope = MapsterConfig.CreateScope();

            UserApplicationService = new UserApplicationService(scope.ServiceProvider.GetService<IMapper>()
                , _userService.Object
                , _recoverPasswordService.Object);
        }
    }
}
