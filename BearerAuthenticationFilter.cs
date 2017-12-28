using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BearerAuthentication
{
    public class BearerAuthenticationFilter : ActionFilterAttribute, IActionFilter
    {
        public BearerAuthenticationFilter()
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (SkipAuthorization(actionContext)) return;

            var headers = actionContext.Request.Headers;

            IEnumerable<string> tempListValues = new List<string>();
            string access_token = string.Empty,
                   client = string.Empty,
                   uid = string.Empty;

            headers.TryGetValues("access_token", out tempListValues);
            access_token = tempListValues?.FirstOrDefault();

            headers.TryGetValues("client", out tempListValues);
            client = tempListValues?.FirstOrDefault();

            headers.TryGetValues("uid", out tempListValues);
            uid = tempListValues?.FirstOrDefault();

            BearerAuthenticationToken tokenFromHeader = new BearerAuthenticationToken()
            {
                access_token = access_token,
                client = client,
                uid = uid
            };

            BearerAuthenticationToken lastToken = BearerSessionManager.GetLastAccessToken();

            if (!tokenFromHeader.Equals(lastToken) || access_token == null)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                base.OnActionExecuting(actionContext);
                return;
            }

            base.OnActionExecuting(actionContext);
        }

        private static bool SkipAuthorization(HttpActionContext actionContext)
        {
            return actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any()
                       || actionContext.ControllerContext.ControllerDescriptor.GetCustomAttributes<AllowAnonymousAttribute>().Any();
        }

        //AFTER CALL ACTION
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            BearerToken bearerToken = new BearerToken();
            BearerAuthenticationToken bearerAuthenticationToken = bearerToken.RefreshAccessToken();

            if(bearerAuthenticationToken.access_token != null)
            {
                actionExecutedContext.Response.Headers.Add("access_token", bearerAuthenticationToken.access_token);
                actionExecutedContext.Response.Headers.Add("client", bearerAuthenticationToken.client);
                actionExecutedContext.Response.Headers.Add("uid", bearerAuthenticationToken.uid);
            }

            base.OnActionExecuted(actionExecutedContext);
        }

    }
}
