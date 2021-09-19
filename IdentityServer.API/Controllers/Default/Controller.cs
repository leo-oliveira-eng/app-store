using Messages.Core;
using Messages.Core.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.API.Controllers.Default
{
    public abstract class Controller : ControllerBase
    {
        protected async Task<IActionResult> WithResponseAsync<TResponseMessage>(Func<Task<Response<TResponseMessage>>> func)
        {
            var response = await func.Invoke();

            if (!response.HasError)
                return Ok(response);

            if (response.Messages.Any(m => m.Type == MessageType.BusinessError))
                return BadRequest(response);

            return StatusCode(500, response);
        }

        protected async Task<IActionResult> WithResponse(Func<Task<Response>> func)
        {
            var response = await func.Invoke();

            if (!response.HasError)
                return Ok(response);

            if (response.Messages.Any(m => m.Type == MessageType.BusinessError))
                return BadRequest(response);

            return StatusCode(500, response);
        }

        protected IActionResult WithResponse(Func<Response> func)
        {
            var response = func.Invoke();

            if (!response.HasError)
                return Ok(response);

            if (response.Messages.Any(m => m.Type == MessageType.BusinessError))
                return BadRequest(response);

            return StatusCode(500, response);
        }
    }
}
