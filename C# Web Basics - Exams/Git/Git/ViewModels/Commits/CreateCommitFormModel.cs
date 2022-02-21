using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Git.ViewModels.Commits
{
    public class CreateCommitFormModel
    {
        public string RepositoryId { get; set; }

        public string CommitId { get; set; }

        public string RepositoryName { get; set; }

        public string Description { get; set; }

        public string CreateDate { get; set; }
    }
}
