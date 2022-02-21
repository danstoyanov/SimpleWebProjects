using MyWebServer.Http;
using MyWebServer.Controllers;

namespace SharedTrip.Controllers
{
    public class HomeController : Controller
    {
        public HttpResponse Index()
        {
            if (this.User.IsAuthenticated)
            {
                return Redirect("/Trips/All");
            }

            return View();
        }
    }
}