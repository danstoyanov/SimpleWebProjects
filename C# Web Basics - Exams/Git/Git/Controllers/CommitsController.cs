using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyWebServer.Http;
using MyWebServer.Controllers;

using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Commits;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly IValidator validator;
        private readonly GitDbContext data;

        public CommitsController(
            IValidator validator,
            GitDbContext data)
        {
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Delete(string Id)
        {
            var commits = this.data.Commits.Find(Id);

            if (commits == null || commits.CreatorId != this.User.Id)
            {
                return BadRequest();
            }

            this.data.Commits.Remove(commits);
            this.data.SaveChanges();

            return Redirect("/Commits/All");
        }

        [Authorize]
        public HttpResponse All()
        {
            var commits = this.data
                .Commits
                .Where(c => c.CreatorId == this.User.Id)
                .OrderByDescending(c => c.CreatedOn)
                .Select(c => new AllCommitsViewModel
                {
                    Id = c.Id,
                    Repository = c.Repository.Name,
                    Description = c.Description,
                    CreatedOn = c.CreatedOn.ToLocalTime().ToString("F")
                })
                .ToList();

            return View(commits);
        }

        [Authorize]
        public HttpResponse Create(string Id)
        {
            var currRepo = this.data
                .Repositories
                .Where(r => r.Id == Id)
                .FirstOrDefault();

            if (currRepo == null)
            {
                return BadRequest();
            }

            var commViewModel = new CreateCommitViewModel
            {
                Id = currRepo.Id,
                Name = currRepo.Name,
            };

            return View(commViewModel);
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateCommitViewModel model)
        {
            var modelErrors = this.validator.ValidateCommit(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var commit = new Commit
            {
                Description = model.Description,
                RepositoryId = model.Id,
                CreatorId = this.User.Id
            };

            this.data.Commits.Add(commit);
            this.data.SaveChanges();

            return View("/Repositories/All");
        }
    }
}
