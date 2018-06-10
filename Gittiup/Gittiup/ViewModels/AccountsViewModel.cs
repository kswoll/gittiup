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

        private readonly GittiupDb db = new GittiupDb();

        public AccountsViewModel()
        {
            Accounts.AddRange(db.Accounts.FindAll());
        }

        public void AddAccount(Account account)
        {
            db.Accounts.Insert(account);
            Accounts.Add(account);
        }
    }
}