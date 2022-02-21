using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWebServer.Controllers;
using MyWebServer.Http;

using SMS.Data;
using SMS.Data.Models;
using SMS.Services;
using SMS.ViewModels.Carts;

namespace SMS.Controllers
{
    public class CartsController : Controller
    {
        private readonly SMSDbContext data;
        private readonly IValidator validator;

        public CartsController(
            SMSDbContext data,
            IValidator validator)
        {
            this.validator = validator;
            this.data = data;
        }


        public HttpResponse Details()
        {
            var user = this.data.Users.Where(u => u.Id == this.User.Id).FirstOrDefault();

            var cart = this.data.Carts
                .Where(c => c.Id == user.CartId)
                .FirstOrDefault();

            var models = cart.Products
                .Select(p => new DetailsCartViewModel
                {
                    Name = p.Name,
                    Price = p.Price
                })
                .ToList();

            return View(models);
        }

        // Buy logic (delete product)
        public HttpResponse AddProduct(string productId)
        {
            var product = this.data.Products.Where(p => p.Id == productId).FirstOrDefault();
            var cart = this.data.Carts.Where(c => c.User.Id == this.User.Id).FirstOrDefault();

            product.CartId = cart.Id;

            this.data.SaveChanges();

            return Redirect("/Carts/Details");
        }
    }
}
