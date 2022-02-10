using MyWebServer.Controllers;
using MyWebServer.Http;

namespace SharedTrip.Controllers
{

    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            return this.View();
        }
    }
}