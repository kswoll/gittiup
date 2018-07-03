using System;
using System.Windows;
using Gittiup.Library.Stores;
using Gittiup.Utils;
using Gittiup.Views;

namespace Gittiup
{
    public class MainWindowBase : BaseWindow<ApplicationStore>
    {
    }

    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new ApplicationStore();

            var settings = Properties.Settings.Default;
            WindowState = settings.WindowState;
            Width = settings.WindowWidth;
            Height = settings.WindowHeight;
        }

        private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            var settings = Properties.Settings.Default;
            settings.WindowState = WindowState;
            settings.WindowWidth = (int)ActualWidth;
            settings.WindowHeight = (int)ActualHeight;
            settings.Save();
        }

        private void MainWindow_OnClosed(object sender, EventArgs e)
        {
            foreach (var child in this.FindVisualDescendants<IDisposable>())
            {
                child.Dispose();
            }
        }

        private void Settings_OnClick(object sender, RoutedEventArgs e)
        {
            repositories.Visibility = Visibility.Collapsed;
            settings.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Visible;
        }

        private void Back_OnClick(object sender, RoutedEventArgs e)
        {
            repositories.Visibility = Visibility.Visible;
            settings.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Collapsed;
        }
    }
}
