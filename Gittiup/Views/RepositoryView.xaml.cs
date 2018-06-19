using System.Windows;
using System.Windows.Controls;
using Gittiup.Models;
using Gittiup.ViewModels;

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

/*
                var branchesNode = new TreeViewNode
                {
                    Content = "Branches"
                };

                foreach (var branch in ViewModel.Repo.Branches)
                {
                    var branchNode = new BoundTreeViewNode<Branch>
                    {
                        Content = branch.FriendlyName,
                        Value = branch
                    };
                    branchesNode.Children.Add(branchNode);
                }

                treeView.RootNodes.Add(branchesNode);
*/
            }
        }

    }
}
