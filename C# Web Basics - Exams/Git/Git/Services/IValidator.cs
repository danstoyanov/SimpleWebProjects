using System.Collections.Generic;

using Git.ViewModels.Users;
using Git.ViewModels.Commits;
using Git.ViewModels.Repostirories;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateRepository(CreateRepositoryViewModel model);

        ICollection<string> ValidateCommit(CreateCommitViewModel model);
    }
}
