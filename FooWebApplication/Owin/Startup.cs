using FooWebApplication.Owin;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using Story.Core;
using System.Configuration;
using System.Linq;
using System.Web.Http;

[assembly: OwinStartup(typeof(Startup))]

namespace FooWebApplication.Owin
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            Storytelling.Factory = new FileBasedStoryFactory(ConfigurationManager.AppSettings["StoryRulesetPath"]);

            appBuilder.Use<StoryMiddleware>();

            ConfigureStaticFiles(appBuilder);

            ConfigureWebApi(appBuilder);
        }

        private static void ConfigureStaticFiles(IAppBuilder appBuilder)
        {
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = new PhysicalFileSystem(".")
            };

            appBuilder.UseFileServer(options);
        }

        private static void ConfigureWebApi(IAppBuilder appBuilder)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();

            // This is to make sure all exceptions are handled (from web api)
            config.Filters.Add(new ExceptionStoryFilterAttribute());

            // Remove xml and keep json
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

            appBuilder.UseWebApi(config);
        }
    }
}
