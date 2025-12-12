using System.Net;
using System.Web.Http.Controllers;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace TodoApi.Filters;

/// <summary>
///     Used for validating models with validation annotations.
/// </summary>
public class ValidateModelAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(HttpActionContext actionContext)
    {
        if (!actionContext.ModelState.IsValid)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, 
                actionContext.ModelState);
        }         
    }
}
