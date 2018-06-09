using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using Gittiup.Controls;
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
//            col.Insert(repo);

            var repos = col.FindAll();


            this.InitializeComponent();


            repo = repos.First();
            navigationView.MenuItems.Insert(0, new NavigationViewItem
            {
                Tag = repo.Id.ToString(),
                Content = new NavigationViewItemContent
                {
                    Text = "Gittiup"
                }
            });
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
                var item = sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Tag == (string)((NavigationViewItem)((NavigationViewItemContent)args.InvokedItem).Parent).Tag);
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
                case "addRepository":
                    ContentFrame.Navigate(typeof(AddRepositoryPage));
                    break;
            }
        }

        private void NavigationView_OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.CanGoBack)
            {
                ContentFrame.GoBack();
            }
        }
    }
}
