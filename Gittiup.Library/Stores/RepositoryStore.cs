using System.Collections.Immutable;
using LibGit2Sharp;

namespace Gittiup.Library.Stores
{
    public class RepositoryStore
    {
        public Repository Repository { get; }
        public ImmutableList<Branch> Branches { get; private set; }
        public BranchStore SelectedBranch { get; private set; }
    }
}