// <copyright file="AddAccountViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Database;

namespace Gittiup.ViewModels
{
    public class EditAccountViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }
        public Account Account { get; set; }

        public void Save()
        {
            bool isNew = Account.Id == 0;

            using (var db = new GittiupDb())
            {
                db.Accounts.Upsert(Account);
            }

            if (isNew)
            {
                Accounts.Add(Account);
            }
        }
    }
}