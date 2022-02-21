using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWebServer.Controllers;
using MyWebServer.Http;

using FootballManager.ViewModels.Players;
using FootballManager.Data.Models;
using FootballManager.Data;
using FootballManager.Services;

namespace FootballManager.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IValidator validator;
        private readonly FootballManagerDbContext data;

        public PlayersController(
            IValidator validator,
            FootballManagerDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse All()
        {
            var playerQuery = this.data
                .Players
                .AsQueryable();

            var players = playerQuery
                .Select(p => new AllPlayerListViewModel
                {
                    Id = p.Id,
                    FullName = p.FullName,
                    Image = p.ImageUrl,
                    Description = p.Description,
                    Position = p.Position,
                    Speed = p.Speed,
                    Endurance = p.Endurance
                })
                .ToList();

            return View(players);
        }

        [Authorize]
        public HttpResponse Add() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddPlayerFormModel model)
        {
            var modelErrors = this.validator.ValidatePlayer(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var player = new Player
            {
                FullName = model.FullName,
                ImageUrl = model.ImageUrl,
                Position = model.Position,
                Speed = model.Speed,
                Endurance = model.Endurance,
                Description = model.Description
            };

            data.Players.Add(player);
            data.SaveChanges();

            return Redirect("/Players/All");
        }

        [Authorize]
        public HttpResponse Collection()
        {
            var userPlayers = this.data
                .Users
                .Where(u => u.Id == this.User.Id)
                .Select(u => new UserPlayersCollectionListViewModel
                {
                    UserPlayers = u.UserPlayers.Select(p => new CollectionViewModel
                    {
                        PlayerId = p.PlayerId,
                        FullName = p.Player.FullName,
                        Description = p.Player.Description,
                        ImageUrl = p.Player.ImageUrl,
                        Position = p.Player.Position,
                        Speed = p.Player.Speed,
                        Endurance = p.Player.Endurance
                    }).ToList()
                })
                .FirstOrDefault();

            if (userPlayers == null)
            {
                return NotFound();
            }

            return View(userPlayers);
        }

        public HttpResponse RemoveFromCollection(string playerId)
        {
            var currUser = this.data.Users
                .Where(u => u.Id == this.User.Id)
                .FirstOrDefault();

            var player = this.data.Players.Find(playerId);

            var userPlayer = new UserPlayer
            {
                User = currUser,
                UserId = currUser.Id,
                Player = player,
                PlayerId = player.Id,
            };

            this.data.UserPlayers.Remove(userPlayer);
            this.data.SaveChanges();

            return Redirect("/Players/Collection");
        }

        [Authorize]
        public HttpResponse AddToCollection(string playerId)
        {
            var currPlayer = this.data
                .Players
                .Where(p => p.Id == playerId)
                .FirstOrDefault();

            var currUser = this.data.
                Users
                .Where(u => u.Id == this.User.Id)
                .FirstOrDefault();

            if (currUser == null || currPlayer == null)
            {
                return NotFound();
            }

            var userPlayer = new UserPlayer
            {
                PlayerId = playerId,
                Player = currPlayer,
                User = currUser,
                UserId = currUser.Id
            };

            if (this.data.UserPlayers.Contains(userPlayer))
            {
                return Redirect("/Players/All");
            }

            this.data.UserPlayers.Add(userPlayer);
            this.data.SaveChanges();

            return Redirect("/Players/All");
        }
    }
}
