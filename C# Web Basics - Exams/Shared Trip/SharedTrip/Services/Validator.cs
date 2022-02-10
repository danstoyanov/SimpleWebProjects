using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using SharedTrip.Models.Users;
using SharedTrip.Models.Trips;

using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateTrip(TripFormModel model)
        {
            var errors = new List<string>();

            if (model.StartPoint.Length < StartPointMinLength || model.StartPoint.Length > StartPointMaxLength)
            {
                errors.Add($"StartPoint '{model.StartPoint}' is not valid. It must be between {StartPointMinLength} and {StartPointMaxLength} characters long.");
            }

            if (model.EndPoint.Length < StartPointMinLength || model.EndPoint.Length > StartPointMaxLength)
            {
                errors.Add($"EndPoint '{model.EndPoint}' is not valid. It must be between {StartPointMinLength} and {StartPointMaxLength} characters long.");
            }

            if (model.Seats < SeatsMinRange || model.Seats > SeatsMaxRange)
            {
                errors.Add($"The {model.Seats} seats number is not valid ! It must be between {SeatsMinRange} and {SeatsMaxRange} range!");
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
