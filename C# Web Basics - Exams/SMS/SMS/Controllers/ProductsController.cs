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
using SMS.ViewModels.Products;

namespace SMS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SMSDbContext data;
        private readonly IValidator validator;

        public ProductsController(
            SMSDbContext data,
            IValidator validator)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Create() => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Create(CreateProductViewForm model)
        {
            var modelErrors = this.validator.ValidateProduct(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var product = new Product()
            {
                Name = model.Name,
                Price = model.Price,
            };

            this.data.Products.Add(product);
            this.data.SaveChanges();

            return Redirect("/Home");
        }
    }
}
