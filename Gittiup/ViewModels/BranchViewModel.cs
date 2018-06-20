using Gittiup.Utils;
using LibGit2Sharp;

namespace Gittiup.ViewModels
{
    public class BranchViewModel : BaseObject
    {
        public Branch Branch { get; }

        public BranchViewModel(Branch branch)
        {
            Branch = branch;
        }
    }
}