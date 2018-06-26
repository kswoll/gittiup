using System;
using System.Windows;

namespace Gittiup
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

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
            throw new NotImplementedException();
        }
    }
}
