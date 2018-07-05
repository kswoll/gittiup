using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Gittiup.Library.Models;
using Gittiup.Library.ViewModels;
using LibGit2Sharp;
using MaterialDesignThemes.Wpf;
using Movel.Ears;

namespace Gittiup.Views
{
    public class BranchViewBase : BaseView<BranchViewModel>
    {
    }

    public partial class BranchView
    {
        public BranchView()
        {
            InitializeComponent();
        }

        protected override void OnViewModelChanged(BranchViewModel oldModel, BranchViewModel newModel)
        {
            base.OnViewModelChanged(oldModel, newModel);
            ViewModel.Listen(x => x.SelectedItemViewModel).Then(OnSelectedItemViewModelChanged);
            ViewModel.Listen(x => x.SelectedFile).Then(OnSelectedFileChanged);
        }

        private void CloseFile_Click(object sender, RoutedEventArgs e)
        {
            commits.Visibility = Visibility.Visible;
            fileView.Visibility = Visibility.Collapsed;
        }

        private void OnSelectedItemViewModelChanged(Ear<BranchItemViewModel> ear, BranchItemViewModel oldValue, BranchItemViewModel newValue)
        {
            if (newValue != null)
            {
                if (newValue.Commit != null)
                {
                    comment.NavigateToString(ViewModel.SelectedCommitMessage);

                    if (newValue.Message.Trim().Equals("wip", StringComparison.InvariantCultureIgnoreCase))
                    {
                        wip.Visibility = Visibility.Visible;
                        comment.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        wip.Visibility = Visibility.Hidden;
                        comment.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    wip.Visibility = Visibility.Collapsed;
                    comment.Visibility = Visibility.Collapsed;
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

        private void Files_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (commits.Visibility != Visibility.Collapsed)
            {
                ShowSelectedFile();
            }
        }

        private void OnSelectedFileChanged(Ear<string> ear, string oldValue, string newValue)
        {
            if (newValue != null)
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
