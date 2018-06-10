// <copyright file="AccountsViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Database;
using Gittiup.Utils;

namespace Gittiup.ViewModels
{
    public class AccountsViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();

        public AccountsViewModel()
        {
            using (var db = new GittiupDb())
            {
                Accounts.AddRange(db.Accounts.FindAll());
            }
        }

        public void DeleteAccount(Account account)
        {
            Accounts.Remove(account);
            using (var db = new GittiupDb())
            {
                db.Accounts.Delete(account.Id);
            }
        }
    }
}