using Story.Core;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace FooWebApplication.Owin
{
    public class ExceptionStoryFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            Storytelling.Warn("Internal error - " + context.Exception);
            var message = context.Exception.Message;
            var httpStatusCode = HttpStatusCode.InternalServerError;

            Storytelling.Data["responseMessage"] = message;

            var resp = new HttpResponseMessage()
            {
                Content = new StringContent(message)
            };

            resp.StatusCode = httpStatusCode;

            context.Response = resp;

            base.OnException(context);
        }
    }
}