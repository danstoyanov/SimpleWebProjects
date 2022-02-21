using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;

using CarShop.Data;
using CarShop.Services;

namespace CarShop
{
    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                .Add<CarShopDbContext>()
                .Add<IViewEngine, CompilationViewEngine>()
                .Add<IValidator, Validator>()
                .Add<IPasswordHasher, PasswordHasher>())
                .WithConfiguration<CarShopDbContext>(context => context
                    .Database.Migrate())
                .Start();
    }
}
