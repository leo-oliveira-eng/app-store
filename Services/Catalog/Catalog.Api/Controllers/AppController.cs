using Catalog.Application.Apps.Contracts;
using Catalog.Messages.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Controller = Catalog.Api.Controllers.Default.Controller;

namespace Catalog.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AppController : Controller
    {
        private IAppApplicationService AppApplicationService { get; }

        public AppController(IAppApplicationService appApplicationService)
        {
            AppApplicationService = appApplicationService ?? throw new ArgumentNullException(nameof(appApplicationService));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAppRequestMessage requestMessage)
            => await WithResponseAsync(() => AppApplicationService.CreateAsync(requestMessage));
    }
}
