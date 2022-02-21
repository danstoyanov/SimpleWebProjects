using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWebServer.Controllers;
using MyWebServer.Http;

using CarShop.Data;
using CarShop.Services;
using CarShop.ViewModels.Issues;
using CarShop.Data.DataModels;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly CarShopDbContext data;
        private readonly IValidator validator;

        public IssuesController(
            CarShopDbContext data,
            IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        public HttpResponse CarIssues(string carId)
        {
            var currCar = this.data
                .Cars
                .Where(c => c.Id == carId)
                .Select(c => new CarToIssuesListViewModel
                {
                    CarId = c.Id,
                    Model = c.Model,
                    Year = c.Year,
                    Issues = c.Issues.Select(i => new ListingCarIssuesViewModel
                    {
                        Id = i.Id,
                        Description = i.Description,
                        CarId = i.CarId,
                        Isfixed = "no",
                        IissueId = i.Id,
                    })
                    .ToList()
                })
                .FirstOrDefault();

            return View(currCar);

        }

        [Authorize]
        public HttpResponse Add() => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Add(string CarId, AddIssueViewModel model)
        {
            var car = this.data
                .Cars
                .Where(c => c.Id == CarId && c.OwnerId == this.User.Id)
                .FirstOrDefault();

            var issue = new Issue
            {
                Description = model.Description,
                CarId = car.Id,
                Car = car,
            };

            this.data.Issues.Add(issue);
            this.data.SaveChanges();

            return Redirect($"/Issues/CarIssues?carId={CarId}");
        }
    }
}

