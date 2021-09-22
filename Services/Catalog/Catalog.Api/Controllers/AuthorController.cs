using Catalog.Api.Filters;
using Catalog.Application.Authors.Contracts;
using Catalog.Messages.Requests;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Controller = Catalog.Api.Controllers.Default.Controller;

namespace Catalog.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class AuthorController : Controller
    {
        private IAuthorApplicationService AuthorApplicationService { get; }

        public AuthorController(IAuthorApplicationService authorApplicationService)
        {
            AuthorApplicationService = authorApplicationService ?? throw new ArgumentNullException(nameof(authorApplicationService));
        }

        [HttpPost, Route("")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateAuthorRequestMessage requestMessage)
            => await WithResponseAsync(() => AuthorApplicationService.CreateAsync(requestMessage));

        [HttpPut, Route("{id}"), RequiredId]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateAuthorRequestMessage requestMessage, Guid id)
            => await WithResponseAsync(() => AuthorApplicationService.UpdateAsync(requestMessage, id));

        [HttpGet, Route("{id}"), RequiredId]
        public async Task<IActionResult> FindAsync(Guid id)
            => await WithResponseAsync(() => AuthorApplicationService.FindAsync(id));

        [HttpDelete, Route("{id}"), RequiredId]
        public async Task<IActionResult> DeleteAsync(Guid id)
            => await WithResponseAsync(() => AuthorApplicationService.DeleteAsync(id));
    }
}
