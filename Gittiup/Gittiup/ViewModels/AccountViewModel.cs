// <copyright file="AddAccountViewModel.cs" company="PlanGrid, Inc.">
//     Copyright (c) 2018 PlanGrid, Inc. All rights reserved.
// </copyright>

using Gittiup.Database;

namespace Gittiup.ViewModels
{
    public class AccountViewModel
    {
        public AccountsViewModel AccountsViewModel { get; set; }
        public Account Account { get; set; }
    }
}