using System.Windows.Input;
using Dragablz;
using Gittiup.Library.Models;
using Gittiup.Library.ViewModels;
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
        }

        private async void Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var repository = new RepositoryModel();
            var addRepository = new EditRepositoryDialog(repository);
            if ((bool)await DialogHost.Show(addRepository, "RootDialog"))
            {
                ViewModel.SaveRepository(repository);
            }
        }
    }
}
