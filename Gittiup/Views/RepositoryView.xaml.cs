using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Gittiup.Library.Models;
using Gittiup.Library.Utils;
using Gittiup.Library.ViewModels;
using LibGit2Sharp;
using Movel.Ears;

namespace Gittiup.Views
{
    public class RepositoryViewBase : BaseView<RepositoryViewModel>
    {
    }

    public partial class RepositoryView
    {
        private readonly BranchView branchView = new BranchView();

        private bool isSelectingNode;

        public RepositoryView()
        {
            InitializeComponent();

            var settings = Properties.Settings.Default;
            sidebarColumn.Width = new GridLength(settings.LeftSidebarWidth);
        }

        protected override void OnViewModelChanged(RepositoryViewModel oldModel, RepositoryViewModel newModel)
        {
            base.OnViewModelChanged(oldModel, newModel);

            newModel.Listen(x => x.SelectedNode).Then(OnSelectedNodeChanged);
            newModel.Listen(x => x.CheckedOutNode).Then(OnCheckedOutNodeChanged);

/*
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
*/
        }

        private TreeViewItem GetTreeViewItem(object node)
        {
            if (node == null)
            {
                return null;
            }

            var stack = new Stack<TreeViewItem>();
            foreach (var item in treeView.Items)
            {
                stack.Push((TreeViewItem)item);
            }

            while (stack.Any())
            {
                var current = stack.Pop();
                if (current.Tag == node)
                {
                    return current;
                }

                foreach (TreeViewItem child in current.Items)
                {
                    stack.Push(child);
                }
            }

            return null;
        }

        private void SetSelectedNode(object node)
        {
            if (isSelectingNode)
                return;

            var item = GetTreeViewItem(node);
            item.IsSelected = true;
        }

        private void OnSelectedNodeChanged()
        {
            var node = ViewModel.SelectedNode;
            SetSelectedNode(node);
            switch (node)
            {
                case Branch branch:
                    branchView.ViewModel = new BranchViewModel(ViewModel.Repo, ViewModel.Repository.Account, branch);
                    branchView.ViewModel.Checkout = ViewModel.Checkout;
                    content.Content = branchView;
                    break;
            }
        }

        private void OnCheckedOutNodeChanged(Ear<object> ear, object oldNode, object newNode)
        {
            var oldItem = GetTreeViewItem(oldNode);
            var newItem = GetTreeViewItem(newNode);

            if (oldItem != null)
            {
                oldItem.FontWeight = FontWeights.Normal;
            }

            if (newItem != null)
            {
                newItem.FontWeight = FontWeights.Bold;
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
/*
            isSelectingNode = true;
            var treeViewItem = (TreeViewItem)e.NewValue;
            ViewModel.SelectedNode = treeViewItem.Tag;
            isSelectingNode = false;
*/
        }

        private void Splitter_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.LeftSidebarWidth = (int)sidebarColumn.ActualWidth;
            settings.Save();
        }
    }
}
