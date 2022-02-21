using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using SMS.ViewModels.Products;
using SMS.ViewModels.Users;

using static SMS.Data.DataConstants;

namespace SMS.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateProduct(CreateProductViewForm model)
        {
            var errors = new List<string>();

            if (model.Name.Length < ProductNameMinLength || model.Name.Length > ProductNameMaxLength)
            {
                errors.Add($"Name '{model.Name}' is not valid. It must be between {ProductNameMinLength} and {ProductNameMaxLength} characters long.");
            }

            if (model.Price < PriceMinValue || model.Price > PriceMaxValue)
            {
                errors.Add($"Price '{model.Name}' is not valid. It must be between {PriceMinValue} to {PriceMaxValue};");
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
