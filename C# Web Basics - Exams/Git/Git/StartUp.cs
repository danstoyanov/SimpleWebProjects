using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Git.Data;
using Git.Services;

using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;

namespace Git
{
    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                .Add<GitDbContext>()
                .Add<IViewEngine, CompilationViewEngine>()
                .Add<IValidator, Validator>()
                .Add<IPasswordHasher, PasswordHasher>())
                .WithConfiguration<GitDbContext>(context => context
                    .Database.Migrate())
                .Start();
    }
}
