using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using SharedTrip.ViewModels.Users;
using SharedTrip.ViewModels.Trips;

using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
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

        public ICollection<string> ValidateTrip (AddTripFormViewModel model)
        {
            var errors = new List<string>();

            if (model.StartPoint.Length < MinStartPointValue || model.StartPoint.Length > MaxStartPointValue)
            {
                errors.Add($"Invalid Sratpoint name, it must be between 1 and 30 characters long !");
            }

            if (model.EndPoint.Length < MinEndPointValue || model.EndPoint.Length > MaxEndPointValue)
            {
                errors.Add($"Invalid EndPoint name, it must be between 1 and 30 characters long !");
            }

            if (model.Seats < MinSeatsValue || model.Seats > MaxSeatsValue)
            {
                errors.Add($"Invalid seats values ! The seats must be between 2 and 6 !");
            }

            return errors;
        }
    }
}
