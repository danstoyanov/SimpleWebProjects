using System.Collections.Generic;

using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateTrip(AddTripFormViewModel model);
    }
}
