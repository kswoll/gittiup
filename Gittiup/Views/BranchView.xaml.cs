using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Gittiup.ViewModels;
using LibGit2Sharp;

namespace Gittiup.Views
{
    public class BranchViewBase : BaseView<BranchViewModel>
    {
    }

    public partial class BranchView
    {
        public BranchView(Branch branch)
        {
            InitializeComponent();

            ViewModel = new BranchViewModel(branch);
//            branch.Commits.ElementAt(0).
        }

        private void Commits_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commits.SelectedItem != null)
            {
                var commit = (Commit)commits.SelectedItem;
                files.ItemsSource = commit.Tree;
                if (rightColumn.ActualWidth == 0)
                {
                    splitterColumn.Width = new GridLength(5);
                    rightColumn.Width = new GridLength(200);
                }
            }
            else
            {
                if (rightColumn.ActualWidth != 0)
                {
                    splitterColumn.Width = new GridLength(0);
                    rightColumn.Width = new GridLength(0);
                }
            }
        }
    }
}
