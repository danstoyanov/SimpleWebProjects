using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SMS.ViewModels.Products;
using SMS.ViewModels.Users;

namespace SMS.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateProduct(CreateProductViewForm model);
    }
}
