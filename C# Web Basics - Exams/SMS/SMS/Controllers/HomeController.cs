using System.Linq;

using MyWebServer.Controllers;
using MyWebServer.Http;

using SMS.Data;
using SMS.ViewModels.Home;
using SMS.ViewModels.Products;

namespace SMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly SMSDbContext data;

        public HomeController(SMSDbContext data)
        {
            this.data = data;
        }

        public HttpResponse Index()
        {
            if (!this.User.IsAuthenticated)
            {
                return View("Index");
            }
            else
            {
                var user = this.data.Users.Where(u => u.Id == this.User.Id)
                    .FirstOrDefault();

                var products = this.data.Products
                    .Select(p => new HomeLoginViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price
                    })
                    .ToList();

                var model = new AllProductsViewModel
                {
                    UserName = user.Username,
                    Products = products
                };

                return View("IndexLoggedIn", model);
            }
        }
    }
}