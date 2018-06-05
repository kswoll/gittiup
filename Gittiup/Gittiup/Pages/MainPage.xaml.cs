using System.Linq;
using Windows.UI.Xaml.Controls;

namespace Gittiup.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
//                ContentFrame.Navigate(typeof(SettingsPage));
            }
            else
            {
                // find NavigationViewItem with Content that equals InvokedItem
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem);
                NavigationView_Navigate(item);
            }
        }

        private void NavigationView_Navigate(NavigationViewItem item)
        {
            switch (item.Tag)
            {
                case "repositories":
                    ContentFrame.Navigate(typeof(RepositoriesPage));
                    break;
            }
        }
    }
}
