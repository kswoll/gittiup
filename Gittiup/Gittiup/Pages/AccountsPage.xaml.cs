using System.Collections.ObjectModel;
using Windows.UI.Xaml;
using Gittiup.Database;
using Gittiup.ViewModels;

namespace Gittiup.Pages
{
    public class AccountsPageBase : BasePage<AccountsViewModel>
    {
    }

    public sealed partial class AccountsPage
    {
        public AccountsPage()
        {
            InitializeComponent();

            ViewModel = new AccountsViewModel
            {
                Accounts = new ObservableCollection<Account>
                {
                    new Account { Name = "test" }
                }
            };
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddAccountPage), ViewModel);
        }
    }
}
