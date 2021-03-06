﻿using System.Windows;
using Gittiup.Library.Models;
using Gittiup.Library.ViewModels;
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

            ViewModel = new AccountsViewModel();
        }

        private async void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            var account = new AccountModel();
            var addAccount = new EditAccountDialog(account);
            await DialogHost.Show(addAccount, "RootDialog");
            ViewModel.SaveAccount(account);
        }

        private async void EditButton_OnClick(object sender, RoutedEventArgs e)
        {
            var account = (AccountModel)accountsListView.SelectedItem;
            var editAccount = new EditAccountDialog(account);
            await DialogHost.Show(editAccount, "RootDialog");
            ViewModel.SaveAccount(account);
        }

        private void RemoveButton_OnClick(object sender, RoutedEventArgs e)
        {
        }
    }
}
