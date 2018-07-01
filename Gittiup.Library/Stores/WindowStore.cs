using System.Collections.Immutable;

namespace Gittiup.Library.Stores
{
    public class WindowStore
    {
        public ImmutableList<BranchStore> Branches { get; set; }
    }
}