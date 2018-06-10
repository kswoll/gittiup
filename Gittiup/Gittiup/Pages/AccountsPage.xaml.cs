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

            ViewModel = new AccountsViewModel();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditAccountPage), new EditAccountViewModel
            {
                Accounts = ViewModel.Accounts,
                Account = new Account()
            });
        }

        private void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            var account = (Account)accountsListView.SelectedValue;
            Frame.Navigate(typeof(EditAccountPage), new EditAccountViewModel
            {
                Accounts = ViewModel.Accounts,
                Account = account
            });
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var account = (Account)accountsListView.SelectedValue;
            ViewModel.DeleteAccount(account);
        }
    }
}
