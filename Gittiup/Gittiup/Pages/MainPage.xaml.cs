using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Gittiup.Database;

namespace Gittiup.Pages
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            var db = new GittiupDb();
            var repo = new Repository
            {
                Path = @"c:\",
                Url = "http://google.com"
            };


            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            var col = db.GetCollection<Repository>("repositories");
            col.Insert(repo);

            var repos = col.FindAll();


            this.InitializeComponent();
        }

        private void NavigationView_OnItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
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
