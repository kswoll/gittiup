using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace Gittiup
{
    /// <summary>
    /// Interaction logic for AddAccountDialog.xaml
    /// </summary>
    public partial class AddAccountDialog
    {
        public AddAccountDialog()
        {
            InitializeComponent();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            await ((MetroWindow)Window.GetWindow(this)).HideMetroDialogAsync(this);
        }

        private async void Cancel_Click(object sender, RoutedEventArgs e)
        {
            await ((MetroWindow)Window.GetWindow(this)).HideMetroDialogAsync(this);
        }
    }
}
