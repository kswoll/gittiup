using System;
using System.Collections.Specialized;
using System.Linq;
using Windows.Storage;
using Windows.UI.Xaml.Controls;
using FontAwesome.UWP;
using Gittiup.Controls;
using Gittiup.Database;
using Gittiup.ViewModels;

namespace Gittiup.Pages
{
    public class MainPageBase : BasePage<MainViewModel>
    {
    }

    public sealed partial class MainPage
    {
        public MainPage()
        {
            ViewModel = new MainViewModel();

            var db = new GittiupDb();
/*
            var repo = new Repository
            {
                Path = @"c:\",
                Url = "http://google.com"
            };
*/


            StorageFolder localFolder = ApplicationData.Current.LocalFolder;

            var col = db.GetCollection<Repository>("repositories");
//            col.Insert(repo);

            var repos = col.FindAll();


            this.InitializeComponent();

            ViewModel.Repositories.CollectionChanged += RepositoriesOnCollectionChanged;

            foreach (var repository in ViewModel.Repositories)
            {
                AddRepository(repository);
            }

//            repo = repos.First();
/*
            navigationView.MenuItems.Insert(0, new NavigationViewItem
            {
                Tag = repo.Id.ToString(),
                Content = new NavigationViewItemContent
                {
                    Text = "Gittiup"
                }
            });
*/
        }

        private void RepositoriesOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Repository repository in e.NewItems)
                    {
                        AddRepository(repository);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Repository repository in e.OldItems)
                    {
                        var item = navigationView.MenuItems.Cast<NavigationViewItem>().Single(x => (string)x.Tag == $"repository-{repository.Id}");
                        navigationView.MenuItems.Remove(item);
                    }
                    break;
            }
        }

        private void AddRepository(Repository repository)
        {
            navigationView.MenuItems.Insert(0, new NavigationViewItem
            {
                Tag = $"repository-{repository.Id}",
                Content = new NavigationViewItemContent
                {
                    Text = "Gittiup",
                    Icon = FontAwesomeIcon.Flag
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
            var tag = (string)item.Tag;
            if (tag.StartsWith("repository-"))
            {
                var repositoryId = int.Parse(tag.Substring("repository-".Length));
                var repository = ViewModel.Repositories.Single(x => x.Id == repositoryId);
                ContentFrame.Navigate(typeof(RepositoryPage), new RepositoryViewModel
                {
                    Repository = repository,
                    Repositories = ViewModel.Repositories
                });
                return;
            }

            switch (tag)
            {
                case "addRepository":
                    ContentFrame.Navigate(typeof(EditRepositoryPage), new EditRepositoryViewModel
                    {
                        Repository = new Repository(),
                        Repositories = ViewModel.Repositories
                    });
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
