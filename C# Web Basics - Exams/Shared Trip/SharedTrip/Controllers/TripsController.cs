using System.Linq;

using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Services;
using SharedTrip.Models.Trips;
using MyWebServer.Http;
using MyWebServer.Controllers;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly SharedTripDbContext data;

        private readonly IValidator validator;

        public TripsController(
            SharedTripDbContext data,
            IValidator validator)
        {
            this.data = data;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var tripsQuery = this.data.Trips.AsQueryable();

            var trips = tripsQuery
                .Select(t => new TripsListViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Add(TripFormModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var trip = new Trip()
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = model.DepartureTime,
                PictureUrl = model.ImagePath,
                Seats = model.Seats,
                Description = model.Description
            };

            this.data.Trips.Add(trip);
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            if (!this.data.Trips.Where(t => t.Id == tripId).Any())
            {
                return Error("Trip does not exist");
            }

            var currTrip = this.data.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new TripsDetailsViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats,
                    Description = t.Description,
                    ImagePath = t.PictureUrl
                })
                .FirstOrDefault();

            return View(currTrip);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            var currUser = this.data.Users
                .Where(u => u.Id == this.User.Id)
                .FirstOrDefault();

            var currTrip = this.data.Trips
                .Where(t => t.Id == tripId)
                .FirstOrDefault();

            if (currUser == null || currTrip == null)
            {
                return BadRequest();
            }

            var userTrip = new UserTrip
            {
                TripId = currTrip.Id,
                Trip = currTrip,
                UserId = currUser.Id,
                User = currUser
            };

            this.data.UserTrips.Add(userTrip);
            this.data.SaveChanges();

            return Redirect("/");
        }
    }
}
