using LibGit2Sharp;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
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