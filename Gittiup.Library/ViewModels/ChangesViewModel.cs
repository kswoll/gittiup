using LibGit2Sharp;

namespace Gittiup.Library.ViewModels
{
    public class ChangesViewModel : BranchItemViewModel
    {
        public RepositoryStatus Status { get; set; }
    }
}