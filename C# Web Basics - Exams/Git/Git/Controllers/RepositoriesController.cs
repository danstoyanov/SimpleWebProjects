using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWebServer.Controllers;
using MyWebServer.Http;

using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Repostirories;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext data;

        public RepositoriesController(
            IValidator validator,
            GitDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        public HttpResponse All()
        {
            var repositoriesQuery = this.data
                .Repositories
                .AsQueryable();

            var repositories = repositoriesQuery
                .OrderByDescending(r => r.CreatedOn)
                .Select(r => new ListRepositoresViewModel
                {
                    Id = r.Id,
                    RepositoryName = r.Name,
                    OwnerName = r.Owner.Username,
                    CreatedOn = r.CreatedOn.ToString("MM/dd/yyyy HH:mm:ss"),
                    CommitsCount = r.Commits.Count(),
                })
                .ToList();

            return View(repositories);
        }

        [Authorize]
        public HttpResponse Create() => View();

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateRepositoryViewModel model)
        {
            var modelErrors = this.validator.ValidateRepository(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var repository = new Repository
            {
                Name = model.Name,
                IsPublic = model.RepositoryType == "Public",
                OwnerId = this.User.Id
            };

            if (this.data.Repositories.Any(r => r.Name == repository.Name && r.Id == repository.Id))
            {
                return Error("This repository already exist !!");
            }

            this.data.Repositories.Add(repository);
            this.data.SaveChanges();

            return Redirect("/Repositories/All");
        }
    }
}
