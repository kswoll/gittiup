using System.Collections.Generic;
using System.Collections.Immutable;
using System.Collections.ObjectModel;
using System.Linq;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
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
        public ImmutableList<RepositoryItemViewModel> Items { get; set; }

        public RepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;
            Repo = new Repository(repository.Path);
            Checkout = this.CreateCommand<Branch>(OnCheckout);

            Repo.DisposeWith(this);

            var items = new List<RepositoryItemViewModel>();

            var branchesNode = new RepositoryItemViewModel
            {
                Name = "Branches",
                IsExpanded = true
            };
            items.Add(branchesNode);

            var branches = new List<RepositoryItemViewModel>();
            foreach (var branch in Repo.Branches.Where(x => !x.IsRemote))
            {
                var branchNode = new RepositoryItemViewModel
                {
                    Name = branch.FriendlyName,
                    Value = branch
                };
                branches.Add(branchNode);

                if (branch.CanonicalName == Repo.Head.CanonicalName)
                {
                    branchNode.IsSelected = true;
                    branchNode.IsCheckedOut = true;
                }
            }
            branchesNode.Children = branches.ToImmutableList();

            if (Repo.Branches.Any(x => x.IsRemote))
            {
                var remotesNode = new RepositoryItemViewModel
                {
                    Name = "Remotes"
                };

                var branchesByRemote = Repo.Branches.Where(x => x.IsRemote).ToLookup(x => x.RemoteName);
                var remotes = new List<RepositoryItemViewModel>();

                foreach (var remote in Repo.Network.Remotes)
                {
                    var remoteNode = new RepositoryItemViewModel
                    {
                        Name = remote.Name
                    };
                    remotes.Add(remoteNode);

                    var remoteBranches = new List<RepositoryItemViewModel>();
                    foreach (var branch in branchesByRemote[remote.Name])
                    {
                        var branchNode = new RepositoryItemViewModel
                        {
                            Name = branch.FriendlyName.ChopStart($"{remote.Name}/"),
                            Value = branch
                        };
                        remoteBranches.Add(branchNode);
                    }

                    remoteNode.Children = remoteBranches.ToImmutableList();
                }

                remotesNode.Children = remotes.ToImmutableList();
                items.Add(remotesNode);
            }

            Items = items.ToImmutableList();
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