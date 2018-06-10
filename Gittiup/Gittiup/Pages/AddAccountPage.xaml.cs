using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gittiup.Database;
using Gittiup.ViewModels;

namespace Gittiup.Pages
{
    public class AccountPageBase : BasePage<AccountViewModel>
    {
    }

    public sealed partial class AccountPage
    {
        public AccountPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel = (AccountViewModel)e.Parameter;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AccountsViewModel.SaveAccount(ViewModel.Account);

            Frame.GoBack();
        }
    }
}
