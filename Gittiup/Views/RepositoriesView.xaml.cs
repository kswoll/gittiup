using System;
using System.Windows.Input;
using Dragablz;
using Gittiup.Models;
using Gittiup.ViewModels;
using LibGit2Sharp;
using MaterialDesignThemes.Wpf;

namespace Gittiup.Views
{
    public class RepositoriesViewBase : BaseView<RepositoriesViewModel>
    {
    }

    public partial class RepositoriesView
    {
        public RepositoriesView()
        {
            InitializeComponent();

            ViewModel = new RepositoriesViewModel();

            repositories.CommandBindings.Clear();
            repositories.CommandBindings.Add(new CommandBinding(TabablzControl.AddItemCommand, Executed));

//            repositories.NewItemFactory = () => new RepositoryModel();
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

//            repositories.Items.Add(new Repository());

//            repositories.GetTemplateChild(TabablzControl.HeaderItemsControlPartName) as DragablzItemsControl
        }

        private async void Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var repository = new RepositoryModel();
            var addRepository = new EditRepositoryDialog(repository);
            await DialogHost.Show(addRepository, "RootDialog");
            ViewModel.SaveRepository(repository);
        }
    }
}
