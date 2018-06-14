using System.Windows;
using MahApps.Metro.Controls;

namespace Gittiup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void HamburgerMenuControl_OnItemClick(object sender, ItemClickEventArgs e)
        {
            hamburgerMenu.Content = e.ClickedItem;
        }
    }
}
