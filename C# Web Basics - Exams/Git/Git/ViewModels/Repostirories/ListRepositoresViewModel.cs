using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.ViewModels.Repostirories
{
    public class ListRepositoresViewModel
    {
        public string Id { get; set; }

        public string RepositoryName { get; set; }

        public string OwnerName { get; set; }

        public string CreatedOn { get; set; }

        public int CommitsCount { get; set; }

    }
}
