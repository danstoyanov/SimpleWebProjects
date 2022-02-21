using CarShop.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.ViewModels.Cars
{
    public class AllCarsListViewModel
    {
        public string CarId { get; set; }

        public string Model{ get; set; }

        public string Image { get; set; }

        public int Year{ get; set; }

        public string PlateNumber{ get; set; }

        public List<Issue> Issues { get; set; } = new List<Issue>();
    }
}
