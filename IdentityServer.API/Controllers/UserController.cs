using IdentityServer.Application.Services.Contracts;
using IdentityServer.Messaging.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Controller = IdentityServer.API.Controllers.Default.Controller;

namespace IdentityServer.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserController : Controller
    {
        public IUserApplicationService UserApplicationService { get; }

        public UserController(IUserApplicationService userApplicationService)
            => UserApplicationService = userApplicationService ?? throw new ArgumentNullException(nameof(userApplicationService));

        [AllowAnonymous, HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserRequestMessage requestMessage)
            => await WithResponseAsync(() => UserApplicationService.CreateAsync(requestMessage));

        [AllowAnonymous, HttpPut, Route("recover-password")]
        public async Task<IActionResult> RecoverPassword([FromBody] RecoverPasswordRequestMessage requestMessage)
            => await WithResponseAsync(() => UserApplicationService.RecoverPasswordAsync(requestMessage));

        [AllowAnonymous, HttpPut, Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestMessage requestMessage) 
            => await WithResponseAsync(() => UserApplicationService.ChangePasswordAsync(requestMessage));
    }
}
