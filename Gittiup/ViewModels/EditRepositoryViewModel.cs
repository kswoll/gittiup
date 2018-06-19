// <copyright file="EditRepositoryViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using System.Linq;
using Gittiup.Models;
using Gittiup.Utils;

namespace Gittiup.ViewModels
{
    public class EditRepositoryViewModel
    {
        public RepositoryModel Repository { get; set; }
        public ObservableCollection<AccountModel> Accounts { get; } = new ObservableCollection<AccountModel>();

        public EditRepositoryViewModel(RepositoryModel repository)
        {
            Repository = repository;

            using (var db = new GittiupDb())
            {
                var accounts = new AccountModel[] { null }.Concat(db.Accounts.FindAll());
                Accounts.AddRange(accounts);
            }
        }
    }
}