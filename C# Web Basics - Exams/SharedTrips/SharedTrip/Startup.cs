using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;

using SharedTrip.Data;
using SharedTrip.Services;

namespace SharedTrip
{
    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                    .Add<SharedTripDbContext>()
                    .Add<IViewEngine, CompilationViewEngine>()
                    .Add<IValidator, Validator>()
                    .Add<IPasswordHasher, PasswordHasher>())
                .WithConfiguration<SharedTripDbContext>(context => context
                    .Database.Migrate())
                .Start();
    }
}
