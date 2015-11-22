using FooWebApplication.Services;
using Story.Core;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace FooWebApplication.Controllers
{
    [RoutePrefix("api")]
    public class FooController : ApiController
    {
        private readonly FooService _fooService = new FooService();

        [Route("something")]
        [HttpGet]
        public Task<HttpResponseMessage> GetSomething()
        {
            return Storytelling.Factory.StartNewAsync("GetSomething", async () =>
            {
                var name = await _fooService.GetRandomName();

                // Log to the story
                Storytelling.Info("Prepare something object");
                object something = new
                {
                    Name = name
                };

                // Add data to story
                Storytelling.Data["something"] = something;

                return Request.CreateResponse(something);
            });
        }

        [Route("error")]
        [HttpGet]
        public HttpResponseMessage ProduceError()
        {
            return Storytelling.Factory.StartNew<HttpResponseMessage>("ProduceError", () =>
           {
               throw new InvalidOperationException("Produce error was called");
           });
        }

        [Route("logs")]
        [HttpGet]
        public Task<HttpResponseMessage> ProduceLogs()
        {
            return Storytelling.Factory.StartNewAsync("ProduceLogs", async () =>
            {
                var str = await _fooService.GetRandomString();
                Storytelling.Debug("This is a debug message {0}", str);
                str = await _fooService.GetRandomString();
                Storytelling.Info("This is a information message {0}", str);
                str = await _fooService.GetRandomString();
                Storytelling.Warn("This is a warning message {0}", str);

                return Request.CreateResponse(HttpStatusCode.Created, "logs created");
            });
        }
    }
}