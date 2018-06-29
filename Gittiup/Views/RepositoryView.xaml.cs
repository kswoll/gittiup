using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using Gittiup.Library.ViewModels;
using LibGit2Sharp;

namespace Gittiup.Views
{
    public class RepositoryViewBase : BaseView<RepositoryViewModel>
    {
    }

    public partial class RepositoryView
    {
        public RepositoryView()
        {
            InitializeComponent();

            DataContextChanged += OnDataContextChanged;

            var settings = Properties.Settings.Default;
            sidebarColumn.Width = new GridLength(settings.LeftSidebarWidth);
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is RepositoryModel repository)
            {
                ViewModel = new RepositoryViewModel(repository);

                var branchesNode = new TreeViewItem
                {
                    Header = "Branches",
                    IsExpanded = true
                };
                treeView.Items.Add(branchesNode);

                foreach (var branch in ViewModel.Repo.Branches.Where(x => !x.IsRemote))
                {
                    var branchNode = new TreeViewItem
                    {
                        Header = branch.FriendlyName,
                        Tag = branch
                    };
                    branchesNode.Items.Add(branchNode);

                    if (branch.CanonicalName == ViewModel.Repo.Head.CanonicalName)
                    {
                        branchNode.IsSelected = true;
                        branchNode.FontWeight = FontWeights.Bold;
                    }
                }

                if (ViewModel.Repo.Branches.Any(x => x.IsRemote))
                {
                    var remotesNode = new TreeViewItem
                    {
                        Header = "Remotes"
                    };
                    treeView.Items.Add(remotesNode);

                    var branchesByRemote = ViewModel.Repo.Branches.Where(x => x.IsRemote).ToLookup(x => x.RemoteName);

                    foreach (var remote in ViewModel.Repo.Network.Remotes)
                    {
                        var remoteNode = new TreeViewItem
                        {
                            Header = remote.Name
                        };
                        remotesNode.Items.Add(remoteNode);

                        foreach (var branch in branchesByRemote[remote.Name])
                        {
                            var branchNode = new TreeViewItem
                            {
                                Header = branch.FriendlyName.ChopStart($"{remote.Name}/"),
                                Tag = branch
                            };
                            remoteNode.Items.Add(branchNode);
                        }
                    }
                }
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeViewItem = (TreeViewItem)e.NewValue;
            switch (treeViewItem.Tag)
            {
                case Branch branch:
                    (content.Content as IDisposable)?.Dispose();
                    content.Content = new BranchView(ViewModel.Repo, ViewModel.Repository.Account, branch);
                    break;
            }
        }

        private void Splitter_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.LeftSidebarWidth = (int)sidebarColumn.ActualWidth;
            settings.Save();
        }
    }
}
