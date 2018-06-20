using Gittiup.Utils;
using LibGit2Sharp;

namespace Gittiup.ViewModels
{
    public class BranchViewModel : BaseObject
    {
        public Repository Repository { get; }
        public Branch Branch { get; }

        public BranchViewModel(Repository repository, Branch branch)
        {
            Repository = repository;
            Branch = branch;
        }
    }
}