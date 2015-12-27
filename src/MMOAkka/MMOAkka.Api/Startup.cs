using System.Reflection;
using System.Threading;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using MMOAkka.Core;
using Owin;

namespace MMOAkka.Api
{
    public class Startup
    {
        public static IContainer CreateKernel()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            var container = builder.Build();
            return container;
        }

        public void Configuration(IAppBuilder app)
        {

            var context = new OwinContext(app.Properties);
            var token = context.Get<CancellationToken>("host.OnAppDisposing");
            if (token != CancellationToken.None)
            {
                token.Register(() =>
                {
                    CharacterActorSystem.Shutdown();
                });
            }

            var config = new HttpConfiguration();
            var container = CreateKernel();
            app.UseAutofacMiddleware(container).UseAutofacWebApi(config);
            WebApiConfig.Register(config);
            app.UseWebApi(config);

            CharacterActorSystem.Create();

            var physicalFileSystem = new PhysicalFileSystem(@"..");
            var options = new FileServerOptions
            {
                EnableDefaultFiles = true,
                FileSystem = physicalFileSystem
            };
            options.StaticFileOptions.FileSystem = physicalFileSystem;
            options.StaticFileOptions.ServeUnknownFileTypes = true;
            options.DefaultFilesOptions.DefaultFileNames = new[]
            {
                "index.html"
            };

            app.UseFileServer(options);


        }

        
    }
}