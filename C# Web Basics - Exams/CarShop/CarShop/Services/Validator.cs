using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using CarShop.ViewModels.Cars;
using CarShop.ViewModels.Issues;
using CarShop.ViewModels.Users;

using static CarShop.Data.DataConstants;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCar(AddCarViewModel model)
        {
            var errors = new List<string>();


            if (model.Model.Length < CarModelMinLength || model.Model.Length > CarModelMaxLength)
            {
                errors.Add($"Car model: '{model.Model}' is not valid. It must be between {CarModelMinLength} and {CarModelMaxLength} characters long.");
            }

            if (model.Year < CarMinYear || model.Year > CarMaxYear)
            {
                errors.Add($"Car year: '{model.Year}' is not valid. It must be between {CarMinYear} and {CarMaxYear} year.");
            }

            if (!Regex.IsMatch(model.PlateNumber, CarPlateNumValidation))
            {
                errors.Add($"Car {model.PlateNumber} is not a valid plate numer.");
            }

            return errors;
        }

        public ICollection<string> ValidateIssue(AddIssueViewModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < IssueMinDescription || model.Description == null)
            {
                errors.Add($"Issue: '{model.Description}' is not valid. It must be between {IssueMinDescription} min characters length.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length < UserMinUsername || model.Username.Length > DefaultMaxLength)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinUsername} and {DefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length < UserMinPassword || model.Password.Length > DefaultMaxLength)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {DefaultMaxLength} characters long.");
            }

            if (model.Password.Any(x => x == ' '))
            {
                errors.Add($"The provided password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add($"Password and its confirmation are different.");
            }

            return errors;
        }
    }
}
