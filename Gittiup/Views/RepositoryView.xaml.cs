using System.Windows;
using System.Windows.Controls;
using Gittiup.Models;
using Gittiup.ViewModels;
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
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is RepositoryModel repository)
            {
                ViewModel = new RepositoryViewModel(repository);

                var branchesNode = new TreeViewItem
                {
                    Header = "Branches"
                };

                foreach (var branch in ViewModel.Repo.Branches)
                {
                    var branchNode = new TreeViewItem
                    {
                        Header = branch.FriendlyName,
                        Tag = branch
                    };
                    branchesNode.Items.Add(branchNode);
                }

                treeView.Items.Add(branchesNode);
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var treeViewItem = (TreeViewItem)e.NewValue;
            switch (treeViewItem.Tag)
            {
                case Branch branch:
                    content.Content = new BranchView(branch);
                    break;
            }
        }
    }
}
