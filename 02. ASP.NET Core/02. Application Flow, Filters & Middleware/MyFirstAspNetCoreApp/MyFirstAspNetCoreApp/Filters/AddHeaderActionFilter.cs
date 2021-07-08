using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MyFirstAspNetCoreApp.Filters
{
    public class AddHeaderActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("X-Info-Action-Name", context.ActionDescriptor.DisplayName);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("X-Info-Result-Type", context.Result.GetType().FullName);
        }
    }
}
