using System.Linq;

using MyWebServer.Http;
using MyWebServer.Controllers;

using Git.Data;
using Git.Data.Models;
using Git.Services;
using Git.ViewModels.Users;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IPasswordHasher passwordHasher;
        private readonly GitDbContext data;

        public UsersController(
            IValidator validator,
            IPasswordHasher passwordHasher,
            GitDbContext data)
        {
            this.validator = validator;
            this.data = data;
            this.passwordHasher = passwordHasher;
        }

        public HttpResponse Login()
        {
            if (this.User.IsAuthenticated)
            {
                return Error("You already loged in !");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Login(LoginUserFormModel model)
        {
            var hashedPassword = this.passwordHasher.HashPassword(model.Password);

            var userId = this.data
                .Users
                .Where(u => u.Username == model.Username && u.Password == hashedPassword)
                .Select(u => u.Id)
                .FirstOrDefault();

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse Register()
        {
            if (this.User.IsAuthenticated)
            {
                return Error("You already registered !");
            }

            return View();
        }

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUser(model);

            if (this.data.Users.Any(u => u.Username == model.Username))
            {
                modelErrors.Add($"User with '{model.Username}' username already exists.");
            }

            if (this.data.Users.Any(u => u.Email == model.Email))
            {
                modelErrors.Add($"User with '{model.Email}' e-mail already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            var user = new User
            {
                Username = model.Username,
                Password = this.passwordHasher.HashPassword(model.Password),
                Email = model.Email,
            };

            data.Users.Add(user);

            data.SaveChanges();

            return Redirect("/Users/Login");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
