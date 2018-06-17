// <copyright file="AddAccountViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Models;

namespace Gittiup.ViewModels
{
    public class EditAccountViewModel
    {
        public ObservableCollection<AccountModel> Accounts { get; set; }
        public AccountModel Account { get; set; }

        public void Save()
        {
            var isNew = Account.Id == 0;

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