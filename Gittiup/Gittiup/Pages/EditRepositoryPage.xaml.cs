using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gittiup.ViewModels;

namespace Gittiup.Pages
{
    public class EditRepositoryPageBase : BasePage<EditRepositoryViewModel>
    {
    }

    public sealed partial class EditRepositoryPage
    {
        public EditRepositoryPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel = (EditRepositoryViewModel)e.Parameter;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Save();

            if (Frame.CanGoBack)
            {
                Frame.GoBack();
            }
            else
            {
                Frame.Navigate(typeof(RepositoryPage), new RepositoryViewModel
                {
                    Repository = ViewModel.Repository
                });
            }
        }
    }
}
