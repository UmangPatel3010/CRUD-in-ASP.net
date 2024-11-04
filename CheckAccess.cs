using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace StaticCRUD
{
    public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Session.GetString("UserID") == null)
            {
                context.Result = new RedirectResult("~/User/UserLogin");
            }
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            context.HttpContext.Response.Headers.Add("Cache-Control", "no-cache, no-store, must-revalidate");
            context.HttpContext.Response.Headers.Add("Expires", "-1");
            context.HttpContext.Response.Headers.Add("Pragma", "no-cache");
            base.OnResultExecuting(context);
        }
    }    
}
