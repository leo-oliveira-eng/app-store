using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Catalog.Api.Filters
{
    public class RequiredIdFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var id = GetCodeFromPath(context.HttpContext.Request.Path);

            if (string.IsNullOrEmpty(id))
                context.Result = new BadRequestObjectResult("Id is invalid");

            context.ActionArguments["id"] = Guid.Parse(id);

            await next();
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        static string GetCodeFromPath(string path)
            => Regex.Match(path.ToUpper(), "[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?").Value;
    }
}
