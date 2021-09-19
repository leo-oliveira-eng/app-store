using Microsoft.AspNetCore.Mvc;

namespace Catalog.Api.Controllers.Default
{
    [ApiController, Route("api/[controller]")]
    public class MeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok(new { name = "Catalog" });
    }
}
