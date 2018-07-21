using System.Collections.Immutable;
using System.Windows;
using System.Windows.Controls.Primitives;
using Gittiup.Library.Utils;
using Gittiup.Library.ViewModels;
using MaterialDesignThemes.Wpf;
using Movel.Ears;

namespace Gittiup.Views
{
    public class NodeViewBase : BaseView<NodeViewModel>
    {
    }

    public partial class NodeView
    {
        private readonly CommitView commitView = new CommitView();
        private readonly ChangesView changesView = new ChangesView();

        public NodeView()
        {
            InitializeComponent();

            commitView.SelectedFileChanged += OnSelectedFileChanged;
            changesView.SelectedFileChanged += OnSelectedFileChanged;
        }

        protected override void OnViewModelChanged(NodeViewModel oldModel, NodeViewModel newModel)
        {
            base.OnViewModelChanged(oldModel, newModel);
            ViewModel.Listen(x => x.SelectedItemViewModel).Then(OnSelectedItemViewModelChanged);
        }

        private void CloseFile_Click(object sender, RoutedEventArgs e)
        {
            commits.Visibility = Visibility.Visible;
            fileView.Visibility = Visibility.Collapsed;
        }

        private void OnSelectedItemViewModelChanged(Ear<NodeItemViewModel> ear, NodeItemViewModel oldValue, NodeItemViewModel newValue)
        {
            if (newValue != null)
            {
                switch (newValue)
                {
                    case CommitNodeItemViewModel commitNodeViewModel:
                        var commitViewModel = new CommitViewModel(ViewModel.Repository, commitNodeViewModel.Commit);
                        commitViewModel.Listen(x => x.SelectedFileContent).Then(() => ViewModel.SelectedFileContent = commitViewModel.SelectedFileContent);
                        commitView.ViewModel = commitViewModel;
                        selectedItemView.Content = commitView;
                        break;
                    case ChangesNodeItemViewModel changesNodeViewModel:
                        var changesViewModel = new ChangesViewModel(ViewModel.Repository, changesNodeViewModel.Status);
                        changesViewModel.Listen(x => x.SelectedFileContent).Then(() => ViewModel.SelectedFileContent = changesViewModel.SelectedFileContent);
                        changesView.ViewModel = changesViewModel;
                        selectedItemView.Content = changesView;
                        break;
                }

                if (rightColumn.Width == new GridLength(0))
                {
                    var settings = Properties.Settings.Default;
                    splitterColumn.Width = new GridLength(5);
                    rightColumn.Width = new GridLength(settings.RightSidebarWidth);
                }
            }
            else
            {
                if (rightColumn.Width != new GridLength(0))
                {
                    splitterColumn.Width = new GridLength(0);
                    rightColumn.Width = new GridLength(0);
                }
            }
        }

        private void OnSelectedFileChanged(ImmutableList<DiffLine> obj)
        {
            if (commits.Visibility != Visibility.Collapsed)
            {
                ShowSelectedFile();
            }
        }

        private void ShowSelectedFile()
        {
            commits.Visibility = Visibility.Collapsed;
            fileView.Visibility = Visibility.Visible;
        }

        private void Splitter_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var settings = Properties.Settings.Default;
            var width = (int)rightColumn.ActualWidth;
            if (width > 0)
            {
                settings.RightSidebarWidth = width;
                settings.Save();
            }
        }

        private async void Branch_OnClick(object sender, RoutedEventArgs e)
        {
            var model = new StringInputViewModel
            {
                Title = "Create Branch"
            };
            var dialog = new StringInputDialog(model);
            var result = (bool)await DialogHost.Show(dialog, "RootDialog");
            if (result)
            {
                ViewModel.CreateBranch(model.Input);
            }
        }
    }
}
