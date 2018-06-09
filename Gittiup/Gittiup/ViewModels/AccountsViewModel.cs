// <copyright file="AccountsViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using System.Collections.ObjectModel;
using Gittiup.Database;

namespace Gittiup.ViewModels
{
    public class AccountsViewModel
    {
        public ObservableCollection<Account> Accounts { get; set; }
    }
}