using Gittiup.Library.Models;
using LibGit2Sharp;
using Movel;
using Movel.Commands;
using Movel.Utils;

namespace Gittiup.Library.ViewModels
{
    public class RepositoryViewModel : BaseObject
    {
        public RepositoryModel Repository { get; }
        public Repository Repo { get; }
        public IAsyncCommand<Branch> Checkout { get; }
        public object SelectedNode { get; set; }
        public object CheckedOutNode { get; set; }

        public RepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;
            Repo = new Repository(repository.Path);
            Checkout = this.CreateCommand<Branch>(OnCheckout);

            AddDisposable(Repo);
        }

        private void OnCheckout(Branch branch)
        {
            var repository = Repo;

            if (!branch.IsRemote)
            {
                Commands.Checkout(repository, branch);
            }
            else
            {
                var origin = repository.Branches[$"origin/{branch}"];
                var local = repository.CreateBranch(branch.CanonicalName, origin.Tip);
                repository.Branches.Update(local, x => x.TrackedBranch = origin.CanonicalName);
                Commands.Checkout(repository, local);
            }

            SelectedNode = branch;
            CheckedOutNode = branch;
        }
    }
}