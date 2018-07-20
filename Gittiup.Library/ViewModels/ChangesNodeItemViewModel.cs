using Gittiup.Library.Models;
using LibGit2Sharp;

namespace Gittiup.Library.ViewModels
{
    public class ChangesNodeItemViewModel : NodeItemViewModel
    {
        public RepositoryStatus Status { get; }

        public ChangesNodeItemViewModel(RepositoryStatus status, AccountModel account)
        {
            Status = status;
            Message = "(Working Copy Changes)";
            Author = account.Email;
        }
    }
}