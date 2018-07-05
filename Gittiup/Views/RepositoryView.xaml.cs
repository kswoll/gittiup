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

//        private bool isSelectingNode;

        public RepositoryView()
        {
            InitializeComponent();

            var settings = Properties.Settings.Default;
            sidebarColumn.Width = new GridLength(settings.LeftSidebarWidth);
        }

        protected override void OnViewModelChanged(RepositoryViewModel oldModel, RepositoryViewModel newModel)
        {
            base.OnViewModelChanged(oldModel, newModel);

            newModel.Listen(x => x.SelectedItem).Then(OnSelectedItemChanged);
//            newModel.Listen(x => x.CheckedOutItem).Then(OnCheckedOutNodeChanged);

            OnSelectedItemChanged();
        }

        private void OnSelectedItemChanged()
        {
            var item = ViewModel.SelectedItem;
            switch (item.Value)
            {
                case Branch branch:
                    branchView.ViewModel = new BranchViewModel(item, ViewModel.Repo, ViewModel.Repository.Account, branch);
                    branchView.ViewModel.Checkout = ViewModel.Checkout;
                    content.Content = branchView;
                    break;
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModel.SelectedItem = (RepositoryItemViewModel)e.NewValue;
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
