using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Gittiup.Database;
using Gittiup.ViewModels;

namespace Gittiup.Pages
{
    public class AddAccountPageBase : BasePage<AddAccountViewModel>
    {
    }

    public sealed partial class AddAccountPage
    {
        public AddAccountPage()
        {
            InitializeComponent();

            ViewModel = new AddAccountViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ViewModel.AccountsViewModel = (AccountsViewModel)e.Parameter;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.AccountsViewModel.AddAccount(new Account
            {
                Name = ViewModel.Name,
                UserName = ViewModel.UserName
            });

            Frame.GoBack();
        }
    }
}
