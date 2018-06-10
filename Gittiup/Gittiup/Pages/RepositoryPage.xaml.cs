using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;
using Gittiup.ViewModels;

namespace Gittiup.Pages
{
    public class RepositoryPageBase : BasePage<RepositoryViewModel>
    {
    }

    public sealed partial class RepositoryPage
    {
        public RepositoryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel = (RepositoryViewModel)e.Parameter;
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel.Delete();
        }
    }
}
