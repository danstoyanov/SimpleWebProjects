using System;
using System.Linq;

using MyWebServer.Http;
using MyWebServer.Controllers;

using SharedTrip.Data;
using SharedTrip.Data.Models;
using SharedTrip.Services;
using SharedTrip.ViewModels.Trips;

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

        public HttpResponse All()
        {
            var tripsQuery = this.data
                .Trips
                .AsQueryable();

            var trips = tripsQuery
                .Select(t => new ListingTripsFormViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString(),
                    Seats = t.Seats
                })
                .ToList();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddTripFormViewModel model)
        {
            var modelErrors = this.validator.ValidateTrip(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var trip = new Trip
            {
                StartPoint = model.StartPoint,
                EndPoint = model.EndPoint,
                DepartureTime = DateTime.Parse(model.DepartureTime),
                Seats = model.Seats,
                Description = model.Description,
                ImagePath = model.ImagePath,
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
                return Error("This trip does not exist !!");
            }

            var tripView = this.data.Trips
                .Where(t => t.Id == tripId)
                .Select(t => new DetailsTripFormViewModel
                {
                    Id = t.Id,
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    Seats = t.Seats,
                    ImagePath = t.ImagePath,
                    Description = t.Description
                })
                .FirstOrDefault();

            return View(tripView);
        }

        public HttpResponse AddUserToTrip(string tripId)
        {
            if (!this.data.Trips.Where(t => t.Id == tripId).Any())
            {
                return Error("Theare is no trip with this Id number !");
            }

            var trip = this.data.Trips
                .Where(t => t.Id == tripId)
                .FirstOrDefault();

            var user = this.data.Users
                .Where(u => u.Id == this.User.Id)
                .FirstOrDefault();

            var userTrip = new UserTrip()
            {
                UserId = user.Id,
                User = user,
                TripId = trip.Id,
                Trip = trip
            };

            if (this.data.UserTrips.Where(u => u.UserId == userTrip.UserId && u.TripId == userTrip.TripId).Any())
            {
                return Error("This User and trips are already added !");
            }

            this.data.UserTrips.Add(userTrip);
            this.data.SaveChanges();

            return Redirect("/Trips/All");
        }
    }
}
