using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SMS.Data.Models;
using SMS.ViewModels.Home;

namespace SMS.ViewModels.Products
{
    public class AllProductsViewModel
    {
        public string UserName { get; set;}

        public List<HomeLoginViewModel> Products { get; set; } = new List<HomeLoginViewModel>();
    }
}
