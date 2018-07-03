using System.Collections.Immutable;
using Gittiup.Library.Models;
using LibGit2Sharp;
using Movel.Stores;
using Movel.Utils;

namespace Gittiup.Library.Stores
{
    public class ApplicationStore : Store
    {
        public ImmutableList<Repository> Repositories { get; private set; }
        public ImmutableList<AccountModel> Accounts { get; set; }
        public RepositoryStore SelectedRepository { get; set; }
    }
}