using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.ViewModels.Issues
{
    public class CarToIssuesListViewModel
    {
        public string CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public List<ListingCarIssuesViewModel> Issues { get; set; }
    }
}
