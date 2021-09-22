using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.API.Controllers.Default
{
    [ApiController, Route("api/[controller]")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { name = "Identity Server" });
    }
}
