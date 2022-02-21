using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Controllers;
using MyWebServer.Http;

using CarShop.Data;
using CarShop.Services;
using CarShop.ViewModels.Cars;
using CarShop.Data.DataModels;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly CarShopDbContext data;
        private readonly IValidator validator;

        public CarsController(
            CarShopDbContext data,
            IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var carsQuery = this.data
                .Cars
                .AsQueryable();

            // make validation !!!

            var cars = carsQuery
                .Select(c => new AllCarsListViewModel
                {
                    CarId = c.Id,
                    Model = c.Model,
                    Image = c.PictureUrl,
                    Year = c.Year,
                    PlateNumber = c.PlateNumber,
                    Issues = c.Issues
                })
                .ToList();

            return View(cars);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddCarViewModel model)
        {
            var modelErrors = this.validator.ValidateCar(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var car = new Car
            {
                Model = model.Model,
                Year = model.Year,
                PictureUrl = model.Image,
                PlateNumber = model.PlateNumber,
                OwnerId = this.User.Id
            };

            this.data.Cars.Add(car);
            this.data.SaveChanges();

            return Redirect("/Cars/All");
        }
    }
}
