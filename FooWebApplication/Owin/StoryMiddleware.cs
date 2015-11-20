using Microsoft.Owin;
using Story.Core;
using System;
using System.Threading.Tasks;

namespace FooWebApplication.Owin
{
    public class StoryMiddleware : OwinMiddleware
    {
        public StoryMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override Task Invoke(IOwinContext context)
        {
            var request = context.Request;
            return Storytelling.StartNewAsync("Request", async () =>
            {
                try
                {
                    Storytelling.Data["RequestUrl"] = request.Uri.ToString();
                    Storytelling.Data["RequestMethod"] = request.Method;
                    Storytelling.Data["UserIp"] = request.RemoteIpAddress;
                    Storytelling.Data["UserAgent"] = request.Headers.Get("User-Agent");
                    Storytelling.Data["Referer"] = request.Headers.Get("Referer");

                    await Next.Invoke(context);

                    Storytelling.Data["Response"] = context.Response.StatusCode;
                }
                catch (Exception e)
                {
                    var m = e.Message;
                    Storytelling.Error(m);
                    throw;
                }
            });
        }
    }
}