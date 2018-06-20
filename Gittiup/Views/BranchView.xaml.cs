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
        public BranchView(Repository repository, Branch branch)
        {
            InitializeComponent();

            ViewModel = new BranchViewModel(repository, branch);
//            branch.Commits.ElementAt(0).
        }

        private void Commits_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (commits.SelectedItem != null)
            {
                var commit = (Commit)commits.SelectedItem;

                var diff = ViewModel.Repository.Diff.Compare<Patch>(commit.Tree, commit.Parents.First().Tree);
                var paths = diff.Select(x => x.Path).ToArray();

                files.ItemsSource = paths;
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

        private void Files_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            commits.Visibility = Visibility.Collapsed;
            file.Visibility = Visibility.Visible;

            var path = (string)files.SelectedItem;
//            var blob = (Blob)entry.Target;
            var history = ViewModel.Repository.Commits.QueryBy(path).ToArray();
            LogEntry current = null;
            LogEntry previous = null;
            var foo = ViewModel.Repository.Commits.Select(x => x.Tree).ToArray();

            var commit = (Commit)commits.SelectedItem;
            foreach (var version in history)
            {
                if (version.Commit.Sha == commit.Sha)
                {
                    current = version;
                }
                else if (current != null)
                {
                    previous = version;
                    break;
                }
            }
        }
    }
}
