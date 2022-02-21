using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Git.ViewModels.Users;
using Git.ViewModels.Repostirories;
using Git.ViewModels.Commits;

using static Git.Data.DataConstants;

namespace Git.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommit(CreateCommitViewModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < CommitMinDescriptionLength)
            {
                errors.Add($"Repo name: '{model.Description}' is not valid. It must be minimum {CommitMinDescriptionLength} characters long.");

            }

            return errors;
        }

        public ICollection<string> ValidateRepository(CreateRepositoryViewModel model)
        {
            var errors = new List<string>();


            if (model.Name == null || model.Name.Length < RepoNameMinLegnth || model.Name.Length > RepoNameMaxLegnth)
            {
                errors.Add($"Repo name: '{model.Name}' is not valid. It must be between {RepoNameMinLegnth} and {RepoNameMaxLegnth} characters long.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username == null || model.Username.Length < UserMinUsername || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinUsername} and {DefaultMaxLength} characters long.");
            }

            if (model.Email == null || !Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email '{model.Email}' is not a valid e-mail address.");
            }

            if (model.Password == null || model.Password.Length < UserMinPassword || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {DefaultMaxLength} characters long.");
            }

            if (model.Password != null && model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("Password and its confirmation are different.");
            }

            return errors;
        }
    }
}
