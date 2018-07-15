using System.Windows;
using System.Windows.Controls.Primitives;
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
        private readonly NodeView nodeView = new NodeView();

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

            OnSelectedItemChanged();
        }

        private void OnSelectedItemChanged()
        {
            var item = ViewModel.SelectedItem;
            switch (item.Value)
            {
                case Branch branch:
                    nodeView.ViewModel = new NodeViewModel(item, ViewModel.Repo, ViewModel.Repository.Account, branch);
                    nodeView.ViewModel.Checkout = ViewModel.Checkout;
                    content.Content = nodeView;
                    break;
            }
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ViewModel.SelectedItem = (RepositoryItemViewModel)e.NewValue;
        }

        private void Splitter_OnDragCompleted(object sender, DragCompletedEventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.LeftSidebarWidth = (int)sidebarColumn.ActualWidth;
            settings.Save();
        }
    }
}
