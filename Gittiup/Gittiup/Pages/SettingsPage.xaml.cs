using Windows.UI.Xaml;

namespace Gittiup.Pages
{
    public sealed partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();

            accountsFrame.Navigate(typeof(AccountsPage));
        }
    }
}
