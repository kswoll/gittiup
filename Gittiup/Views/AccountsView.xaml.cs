using System.Windows;
using System.Windows.Controls;
using Gittiup.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MaterialDesignThemes.Wpf;

namespace Gittiup.Views
{
    public class AccountsViewBase : BaseView<AccountsViewModel>
    {
    }

    public partial class AccountsView
    {
        public AccountsView()
        {
            InitializeComponent();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var addAccount = new AddAccountDialog();
            await ((MetroWindow)Window.GetWindow(this)).ShowMetroDialogAsync(addAccount);
        }
    }
}
