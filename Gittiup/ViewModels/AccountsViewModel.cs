// <copyright file="AccountsViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Models;
using Gittiup.Utils;

namespace Gittiup.ViewModels
{
    public class AccountsViewModel
    {
        public ObservableCollection<AccountModel> Accounts { get; set; } = new ObservableCollection<AccountModel>();

        public AccountsViewModel()
        {
            using (var db = new GittiupDb())
            {
                Accounts.AddRange(db.Accounts.FindAll());
            }
        }

        public void DeleteAccount(AccountModel account)
        {
            Accounts.Remove(account);
            using (var db = new GittiupDb())
            {
                db.Accounts.Delete(account.Id);
            }
        }
    }
}